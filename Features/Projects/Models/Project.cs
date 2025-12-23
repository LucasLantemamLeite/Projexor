using Stokify.Features.Projects.Enums;
using Stokify.Shared.Base;

// ReSharper disable All

namespace Stokify.Features.Projects.Models;

public sealed class Project : Entity
{
    public string Name { get; private set; } = null!;
    public EStatus Status { get; private set; } =  EStatus.InProgress;
    public DateTime? CompleteDate { get; private set; }
    public Guid? UserId { get; private set; }
    public Guid? GroupId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public Project(string name, int status, DateTime? completeDate, Guid? userId, Guid? groupId)
    {
        Name = name;
        Status = (EStatus)status;
        CompleteDate = completeDate;
        UserId = userId;
        GroupId = groupId;
    }
    
    public Project(Guid id, string name, int status, DateTime? completeDate, Guid? userId, Guid? groupId, DateTime created) : base(id)
    {
        Name = name;
        Status = (EStatus)status;
        CompleteDate = completeDate;
        UserId = userId;
        GroupId = groupId;
        CreatedAt = created;
    }
    
    private Project() {}
}