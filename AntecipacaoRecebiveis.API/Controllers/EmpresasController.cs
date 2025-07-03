using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.API.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresasController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        // Lógica para obter a lista de produtos
        var produtos = new[] { "Produto 1", "Produto 2" };
        return Ok(produtos); // Retorna um status 200 OK com a lista de produtos
    }
}
