using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
namespace AntecipacaoRecebiveis.Infrastructure.Repositories;

public class NotaFiscalRepository : INotaFiscalRepository
{
    private readonly AppDbContext _context;
    public NotaFiscalRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<NotaFiscal?> CadastrarNFAsync(NotaFiscal nota)
    {
        await _context.NotasFiscais.AddAsync(nota);
        return nota;
    }
    public async Task<NotaFiscal?> ObterNFPorIdAsync(Guid id)
    {
        return await _context.NotasFiscais.FindAsync(id);
    }
}
