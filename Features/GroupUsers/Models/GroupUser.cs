using Stokify.Features.Groups.Models;
using Stokify.Features.Users.Models;
using Stokify.Shared.Base;
using Stokify.Shared.Enums;

namespace Stokify.Features.GroupUsers.Models;

public sealed class GroupUser : Entity
{
    public Guid UserId { get; private set; }
    public Guid GroupId { get; private set; }
    public ERole Role { get; private set; } = ERole.Member;
    public Group? Group { get; private set; }
    public User? User { get; private set; }

    public GroupUser(Guid userId, Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }

    public GroupUser(Guid id, Guid userId, Guid groupId, int role) : base(id)
    {
        UserId = userId;
        GroupId = groupId;
        Role = (ERole)role;
    }

    private GroupUser() { }
}