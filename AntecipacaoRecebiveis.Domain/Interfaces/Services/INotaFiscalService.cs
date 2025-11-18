using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalDto> CriarNotaFiscal(NotaFiscalDto dto);
        Task<NotaFiscal?> ObterNFPorId(Guid id);
    }
}
