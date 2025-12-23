namespace Stokify.Shared.Base;

public abstract class Entity
{
    protected Guid Id { get; set; }
    
    protected Entity()
        => Id = Guid.NewGuid();
    
    // ReSharper disable once ConvertToPrimaryConstructor
    protected Entity(Guid id)
        => Id = id;
}