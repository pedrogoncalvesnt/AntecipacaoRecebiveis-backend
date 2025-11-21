using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;
using AntecipacaoRecebiveis.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.API.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresaService _empresaService;
    private readonly INotaFiscalService _notaFiscalService;

    public EmpresasController(IEmpresaService empresaService, INotaFiscalService notaFiscalService)
    {
        _empresaService = empresaService;
        _notaFiscalService = notaFiscalService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastraEmpresa([FromBody] CriarEmpresaRequest request)
    {
        var empresa = await _empresaService.CriarEmpresa(request);
        return CreatedAtAction(nameof(ObterEmpresaPorId), new { id = empresa.Id }, empresa);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterEmpresaPorId(Guid id)
    {
        var empresa = await _empresaService.ObterEmpresaPorId(id);
        if (empresa == null) return NotFound(new { error = "Empresa não encontrada" });
        return Ok(empresa);
    }

    [HttpPost("{empresaId:guid}/carrinho/notas/{notaId:guid}")]
    public async Task<IActionResult> AdicionarNotaExistenteAoCarrinho(Guid empresaId, Guid notaId)
    {
        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null) return NotFound(new { error = "Empresa não encontrada" });

        var notaExistente = await _notaFiscalService.ObterNFPorId(notaId);
        if (notaExistente == null) return NotFound(new { error = "Nota fiscal não encontrada" });

        var added = await _notaFiscalService.AdicionarNotaExistenteAoCarrinhoAsync(empresaId, notaId);
        if (added == null)
            return UnprocessableEntity(new { error = "Não foi possível adicionar a nota (inválida, antecipada, limite excedido ou já em carrinho)." });

        return Ok(added);
    }

    [HttpDelete("{empresaId:guid}/carrinho/notas/{notaId:guid}")]
    public async Task<IActionResult> RemoverNotaDoCarrinho(Guid empresaId, Guid notaId)
    {
        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null) return NotFound(new { error = "Empresa não encontrada" });

        var removed = await _notaFiscalService.RemoverDoCarrinhoAsync(empresaId, notaId);
        if (!removed) return NotFound(new { error = "Nota não encontrada no carrinho da empresa" });

        return NoContent();
    }

    [HttpGet("{empresaId:guid}/carrinho")]
    public async Task<IActionResult> ObterCarrinhoDaEmpresa(Guid empresaId)
    {
        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null) return NotFound(new { error = "Empresa não encontrada" });

        var itensDto = await _notaFiscalService.ObterCarrinhoAsync(empresaId);
        return Ok(itensDto);
    }

    [HttpPost("efetivar-antecipacao")]
    public async Task<IActionResult> EfetivarAntecipacao([FromQuery] Guid empresaId)
    {
        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null) return NotFound(new { error = "Empresa não encontrada" });

        var result = await _notaFiscalService.EfetivarAntecipacaoAsync(empresaId);
        return Ok(result);
    }
}