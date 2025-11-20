using AntecipacaoRecebiveis.Application.Services;
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
        if (empresa == null)
            return NotFound();

        return Ok(empresa);
    }

    [HttpPost]
    [Route("/{empresaId}/carrinho/itens")]
    public async Task<IActionResult> AdicionarItemAoCarrinho(Guid empresaId, [FromBody] CriarNotaFiscalRequest notaFiscalDto)
    {
        if (notaFiscalDto.EmpresaId != empresaId)
            return BadRequest("EmpresaId da nota deve corresponder.");

        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null)
            return NotFound();

        var added = await _notaFiscalService.AdicionarAoCarrinhoAsync(empresaId, notaFiscalDto);
        if (added == null)
        {
            // Falha genérica: validação, limite excedido ou nota inválida|
            return UnprocessableEntity(new { error = "Não foi possível adicionar a nota ao carrinho (nota inválida, já em outro carrinho ou limite excedido)." });
        }

        return Ok(added);
    }

    [HttpDelete]
    [Route("/{empresaId}/carrinho/itens/{notaId}")]
    public async Task<IActionResult> RemoverItemDoCarrinho(Guid empresaId, Guid notaId)
    {
        // verifica se a empresa existe
        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null)
            return NotFound();

        var removed = await _notaFiscalService.RemoverDoCarrinhoAsync(empresaId, notaId);
        if (!removed)
            return NotFound();

        return NoContent();
    }

    [HttpGet]
    [Route("/{empresaId}/carrinho")]
    public async Task<IActionResult> ObterCarrinhoDaEmpresa(Guid empresaId)
    {
        // verifica se a empresa existe
        var empresa = await _empresaService.ObterEmpresaPorId(empresaId);
        if (empresa == null)
            return NotFound();

        var itensDto = await _notaFiscalService.ObterCarrinhoAsync(empresaId);
        return Ok(itensDto);
    }
}