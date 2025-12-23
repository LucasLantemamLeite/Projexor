using Stokify.Shared.Base;
using Stokify.Shared.Enums;

namespace Stokify.Models;

public sealed class GroupUsers : Entity
{
    public Guid? UserId { get; private set; }
    public Guid? GroupId { get; private set; }
    public ERole Role { get; private set; } = ERole.Member;

    public GroupUsers(Guid userId, Guid groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
    
    public GroupUsers(Guid id, Guid userId, Guid groupId, int role) : base(id)
    {
        UserId = userId;
        GroupId = groupId;
        Role = (ERole)role;
    }
    
    private GroupUsers() { }
}