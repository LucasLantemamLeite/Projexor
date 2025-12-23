using Stokify.Features.GroupUsers.Models;
using Stokify.Features.Projects.Models;
using Stokify.Shared.Base;

namespace Stokify.Features.Users.Models;

public sealed class User : Entity
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public bool IsSuperAdmin { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public bool Active { get; private set; } = true;
    public ICollection<Project> Projects { get; private set; } = [];
    public ICollection<GroupUser> GroupUsers { get; private set; } = [];

    public User(string name, string email, string phone, string password, bool isSuperAdmin)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Password = password;
        IsSuperAdmin = isSuperAdmin;
    }

    public User(Guid id, string name, string email, string phone, string password, bool isSuperAdmin, DateTime created, bool active) : base(id)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Password = password;
        IsSuperAdmin = isSuperAdmin;
        CreatedAt = created;
        Active = active;
    }

    private User() { }
}