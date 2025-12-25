using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projexor.Data.Context;
using Projexor.Features.Groups.Dto;
using Projexor.Features.Users.Auth;
using Projexor.Features.Users.Enums;

namespace Projexor.Features.Groups.Controllers;

[ApiController]
[Route("v1/group/update")]
[Tags("Update")]
public sealed class UpdateGroupController(AppDbContext context) : ControllerBase
{
    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> ExecuteAsync([FromBody] UpdateGroupDto updateDto, CancellationToken cancellationToken = default)
    {
        if (updateDto.GetType().GetProperties().Where(p => p.Name != "ConfirmPassword" && p.Name != "GroupId").All(p => p.GetValue(updateDto) is null))
            return NoContent();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var group = await context.Groups
            .SingleOrDefaultAsync(x => x.Id == updateDto.GroupId, cancellationToken);

        if (group is null)
            return NotFound(new { message = "Group not found." });

        var owner = await context.GroupUsers
            .Include(x => x.User)
            .Where(x => x.GroupId == group.Id && x.Role == ERole.Owner)
            .Select(x => x.User)
            .SingleOrDefaultAsync(cancellationToken);

        if (owner is null)
            return NotFound(new { message = "Group owner not found." });

        if (!Hasher.VerifyHash(owner.Password, updateDto.ConfirmPassword))
            return Unauthorized(new { message = "Invalid password." });

        group.ChangeName(updateDto.Name);
        group.ChangeActive(updateDto.Active);

        context.Groups.Update(group);
        await context.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Group updated successfully." });
    }
}