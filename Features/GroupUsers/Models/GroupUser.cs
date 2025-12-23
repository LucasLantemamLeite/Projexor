using Projexor.Features.Groups.Models;
using Projexor.Features.Users.Enums;
using Projexor.Features.Users.Models;
using Projexor.Shared.Base;

namespace Projexor.Features.GroupUsers.Models;

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