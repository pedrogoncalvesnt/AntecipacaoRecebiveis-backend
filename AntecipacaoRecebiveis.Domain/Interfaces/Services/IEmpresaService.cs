using AntecipacaoRecebiveis.Domain.DTOs;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<EmpresaDto> CriarEmpresa(CriarEmpresaDto dto);
        Task<EmpresaDto?> ObterEmpresaPorId(Guid id);
    }
}
