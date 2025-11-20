using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Requests;

namespace AntecipacaoRecebiveis.Domain.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<EmpresaDto> CriarEmpresa(CriarEmpresaRequest request);
        Task<Empresa?> ObterEmpresaPorId(Guid id);
    }
}
