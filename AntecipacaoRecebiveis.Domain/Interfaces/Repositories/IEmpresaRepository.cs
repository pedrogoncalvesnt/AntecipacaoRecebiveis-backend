using AntecipacaoRecebiveis.Domain.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
namespace AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
public interface IEmpresaRepository
{
    Task<Empresa> CadastrarEmpresaAsync(EmpresaDto dto);
    Task<Empresa?> ObterEmpresaPorIdAsync(Guid id);
}