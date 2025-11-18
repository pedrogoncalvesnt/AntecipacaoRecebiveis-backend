using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<EmpresaDto> CriarEmpresa(EmpresaDto dto);
        Task<Empresa?> ObterEmpresaPorId(Guid id);
    }
}
