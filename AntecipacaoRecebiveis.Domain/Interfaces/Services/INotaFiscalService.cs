using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Requests;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface INotaFiscalService
    {
        Task<NotaFiscalDto> CriarNotaFiscal(CriarNotaFiscalRequest dto);
        Task<NotaFiscalDto?> ObterNFPorId(Guid id);
        Task<NotaFiscalDto?> AdicionarAoCarrinhoAsync(Guid empresaId, CriarNotaFiscalRequest notaFiscalDto);
        Task<bool> RemoverDoCarrinhoAsync(Guid empresaId, Guid notaId);
        Task<IEnumerable<NotaFiscalDto>> ObterCarrinhoAsync(Guid empresaId);
    }
}
