using AntecipacaoRecebiveis.Domain.Entities;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Repositories
{
    public interface INotaFiscalRepository
    {
        Task<NotaFiscal?> CadastrarNFAsync(NotaFiscal nota);
        Task<NotaFiscal?> ObterNFPorIdAsync(Guid id);
    }
}