using AntecipacaoRecebiveis.Domain.DTOs;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalDto> CriarNotaFiscal(CriarNotaFiscalDto dto);
        Task<NotaFiscalDto?> ObterNFPorId(Guid id);
    }
}
