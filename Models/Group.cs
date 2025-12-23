using Stokify.Shared.Base;

namespace Stokify.Models;

public sealed class Group : Entity
{
    public string Name { get; private set; } = null!;
    public DateTime CreatedAt { get ; set; } =  DateTime.UtcNow;
    public bool Active { get; private set; } = true;
    public List<Project> Projects { get; private set; } = [];

    public Group(string name)
    {
        Name = name;
    }   
    
    public Group(Guid id, string name, DateTime created, bool active) : base(id)
    {
        Name = name;
        CreatedAt = created;
        Active = active;
    }

    private Group() {}
}