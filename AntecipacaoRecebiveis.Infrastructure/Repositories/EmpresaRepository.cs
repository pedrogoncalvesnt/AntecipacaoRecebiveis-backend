using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using AntecipacaoRecebiveis.Domain.Requests;
namespace AntecipacaoRecebiveis.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _context;
    public EmpresaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Empresa> CadastrarEmpresaAsync(CriarEmpresaRequest request)
    {
        var empresa = Empresa.FromRequest(request);

        await _context.Empresas.AddAsync(empresa);
        return empresa;
    }

    public async Task<Empresa?> ObterEmpresaPorIdAsync(Guid id)
    {
        return await _context.Empresas.FindAsync(id);
    }
}
