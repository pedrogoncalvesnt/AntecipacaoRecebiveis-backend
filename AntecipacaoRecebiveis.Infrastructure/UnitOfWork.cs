using AntecipacaoRecebiveis.Domain.Interfaces.Data;

namespace AntecipacaoRecebiveis.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            // Log the exception (not implemented here)
            throw new Exception("An error occurred while saving changes.", ex);
        }
    }
}
