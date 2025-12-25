using Projexor.Features.GroupUsers.Models;
using Projexor.Features.Projects.Models;
using Projexor.Shared.Base;

namespace Projexor.Features.Groups.Models;

public sealed class Group : Entity
{
    public string Name { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool Active { get; private set; } = true;
    public ICollection<Project> Projects { get; private set; } = [];
    public ICollection<GroupUser> GroupUsers { get; private set; } = [];

    public void ChangeName(string? name)
    {
        if (name is not null && Name != name)
            Name = name;
    }

    public void ChangeActive(bool? active)
    {
        if (active is not null && Active != active)
            Active = active.Value;
    }

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

    private Group() { }
}