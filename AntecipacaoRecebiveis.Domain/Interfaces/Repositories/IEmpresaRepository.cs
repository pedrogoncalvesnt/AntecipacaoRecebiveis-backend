using AntecipacaoRecebiveis.Domain.Entities;
namespace AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
public interface IEmpresaRepository
{
    Task<Empresa?> CadastrarEmpresaAsync(Empresa empresa);
    Task<Empresa?> ObterEmpresaPorIdAsync(Guid id);
}