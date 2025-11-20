using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Requests;
namespace AntecipacaoRecebiveis.Domain.Interfaces.Repositories;
public interface IEmpresaRepository
{
    Task<Empresa> CadastrarEmpresaAsync(CriarEmpresaRequest empresa);
    Task<Empresa?> ObterEmpresaPorIdAsync(Guid id);
}