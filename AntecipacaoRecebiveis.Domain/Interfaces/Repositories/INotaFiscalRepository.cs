using AntecipacaoRecebiveis.Domain.Entities;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Repositories
{
    public interface INotaFiscalRepository
    {
        Task<NotaFiscal> CadastrarAsync(NotaFiscal nota);
        Task<NotaFiscal?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<NotaFiscal>> ObterPorCarrinhoIdAsync(Guid carrinhoId);
        Task<IEnumerable<NotaFiscal>> ObterPorEmpresaIdAsync(Guid empresaId);
        Task<decimal> SomarValorPorCarrinhoIdAsync(Guid carrinhoId);
        Task AtualizarAsync(NotaFiscal nota);
    }
}