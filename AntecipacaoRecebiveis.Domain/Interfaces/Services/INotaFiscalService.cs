using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Requests;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalDto> CriarNotaFiscal(CriarNotaFiscalRequest dto);
        Task<NotaFiscalDto?> ObterNFPorId(Guid id);
        Task<NotaFiscalDto?> AdicionarNotaExistenteAoCarrinhoAsync(Guid empresaId, Guid notaId);
        Task<bool> RemoverDoCarrinhoAsync(Guid empresaId, Guid notaId);
        Task<IEnumerable<NotaFiscalDto>> ObterCarrinhoAsync(Guid empresaId);
        Task<EfetivacaoAntecipacaoResponse> EfetivarAntecipacaoAsync(Guid empresaId);
    }
}
