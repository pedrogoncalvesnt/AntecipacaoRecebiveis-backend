using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
namespace AntecipacaoRecebiveis.Infrastructure.Repositories;

public class NotaFiscalRepository : INotaFiscalRepository
{
    private readonly AppDbContext _context;
    public NotaFiscalRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<NotaFiscal> CadastrarAsync(NotaFiscal notaFiscal)
    {
        await _context.NotasFiscais.AddAsync(notaFiscal);
        return notaFiscal;
    }
    public async Task<NotaFiscal?> ObterPorIdAsync(Guid id)
    {
        return await _context.NotasFiscais.FindAsync(id);
    }

    public async Task<IEnumerable<NotaFiscal>> ObterPorCarrinhoIdAsync(Guid carrinhoId)
    {
        return await _context.NotasFiscais
            .Where(n => n.CarrinhoId.HasValue && n.CarrinhoId.Value == carrinhoId)
            .ToListAsync();
    }

    public async Task<IEnumerable<NotaFiscal>> ObterPorEmpresaIdAsync(Guid empresaId)
    {
        return await _context.NotasFiscais
            .Where(n => n.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<decimal> SomarValorPorCarrinhoIdAsync(Guid carrinhoId)
    {
        var soma = await _context.NotasFiscais
            .Where(n => n.CarrinhoId.HasValue && n.CarrinhoId.Value == carrinhoId)
            .SumAsync(n => (decimal?)n.Valor);

        return soma ?? 0m; // retorna 0 se não houver notas fiscais
    }

    public Task AtualizarAsync(NotaFiscal nota)
    {
        _context.NotasFiscais.Update(nota);
        return Task.CompletedTask;
    }
}
