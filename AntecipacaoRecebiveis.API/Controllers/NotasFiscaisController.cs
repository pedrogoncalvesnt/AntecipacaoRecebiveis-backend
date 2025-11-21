using AntecipacaoRecebiveis.Domain.Interfaces.Services;
using AntecipacaoRecebiveis.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.API.Controllers;

[ApiController]
[Route("api/notasfiscais")]
public class NotasFiscaisController : ControllerBase
{
    private readonly INotaFiscalService _nfService;

    public NotasFiscaisController(INotaFiscalService nfService)
    {
        _nfService = nfService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastraNf([FromBody] CriarNotaFiscalRequest request)
    {
        var nota = await _nfService.CriarNotaFiscal(request);
        return CreatedAtAction(nameof(ObterNotaPorId), new { id = nota.EmpresaId }, nota);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterNotaPorId(Guid id)
    {
        var nota = await _nfService.ObterNFPorId(id);
        if (nota == null)
            return NotFound();

        return Ok(nota);
    }
}