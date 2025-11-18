using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Repositories
{
    public interface INotaFiscalRepository
    {
        Task<NotaFiscal> CadastrarNFAsync(NotaFiscalDto nota);
        Task<NotaFiscal?> ObterNFPorIdAsync(Guid id);
    }
}