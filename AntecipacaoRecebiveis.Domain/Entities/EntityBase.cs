namespace AntecipacaoRecebiveis.Domain.Entities;
public abstract class EntityBase
{
    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
}
