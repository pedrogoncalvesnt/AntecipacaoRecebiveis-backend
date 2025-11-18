using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.API.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresasController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastraEmpresa([FromBody] EmpresaDto dto)
    {
        var empresa = await _empresaService.CriarEmpresa(dto);
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
}
