using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projexor.Data.Context;
using Projexor.Features.Groups.Dto;
using Projexor.Features.Groups.Models;
using Projexor.Features.GroupUsers.Models;

namespace Projexor.Features.Groups.Controllers;

[ApiController]
[Route("v1/group/create")]
[Tags("Create")]
public sealed class CreateGroupController(AppDbContext context) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ExecuteAsync([FromBody] CreateGroupDto createDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out Guid userId))
            return Unauthorized(new { message = "Invalid user identifier." });

        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken);

        if (user is null)
            return NotFound(new { message = "User not found." });

        var group = new Group(
            name: createDto.Name
        );

        var groupUser = new GroupUser(
            userId: user.Id,
            groupId: group.Id,
            role: 2
        );

        context.Groups.Add(group);
        context.GroupUsers.Add(groupUser);

        await context.SaveChangesAsync(cancellationToken);

        return Created("", new { message = "Group created successfully!" });
    }
}