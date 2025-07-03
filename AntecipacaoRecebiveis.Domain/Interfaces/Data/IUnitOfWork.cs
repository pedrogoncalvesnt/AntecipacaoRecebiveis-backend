namespace AntecipacaoRecebiveis.Domain.Interfaces.Data;
public interface IUnitOfWork
{
     Task<bool> SaveChangesAsync();
}
