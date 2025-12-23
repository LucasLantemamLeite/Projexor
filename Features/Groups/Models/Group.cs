using Stokify.Features.Projects.Models;
using Stokify.Shared.Base;
using Stokify.Features.GroupUsers.Models;

namespace Stokify.Features.Groups.Models;

public sealed class Group : Entity
{
    public string Name { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool Active { get; private set; } = true;
    public ICollection<Project> Projects { get; private set; } = [];
    public ICollection<GroupUser> GroupUsers { get; private set; } = [];

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