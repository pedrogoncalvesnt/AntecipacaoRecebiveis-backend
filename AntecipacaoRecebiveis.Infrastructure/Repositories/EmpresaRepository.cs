using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
namespace AntecipacaoRecebiveis.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _context;
    public EmpresaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Empresa> CadastrarEmpresaAsync(EmpresaDto dto)
    {
        var empresa = Empresa.FromDto(dto);

        await _context.Empresas.AddAsync(empresa);
        return empresa;
    }

    public async Task<Empresa?> ObterEmpresaPorIdAsync(Guid id)
    {
        return await _context.Empresas.FindAsync(id);
    }
}
