using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projexor.Data.Context;
using Projexor.Features.Groups.Dto;
using Projexor.Features.Users.Auth;
using Projexor.Features.Users.Enums;

namespace Projexor.Features.Groups.Controllers;

[ApiController]
[Route("v1/group/delete")]
[Tags("Delete")]
public sealed class DeleteGroupController(AppDbContext context) : ControllerBase
{
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> ExecuteAsync([FromBody] DeleteGroupDto deleteDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var group = await context.Groups
            .SingleOrDefaultAsync(x => x.Id == deleteDto.GroupId, cancellationToken);

        if (group is null)
            return NotFound(new { message = "Group not found." });

        var owner = await context.GroupUsers
            .Include(x => x.User)
            .Where(x => x.GroupId == group.Id && x.Role == ERole.Owner)
            .Select(x => x.User)
            .SingleOrDefaultAsync(cancellationToken);

        if (owner is null)
            return NotFound(new { message = "Owner not found." });

        if (!Hasher.VerifyHash(owner.Password, deleteDto.ConfirmPassword))
            return Unauthorized(new { message = "Invalid password." });

        context.Groups.Remove(group);
        await context.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "Group deleted successfully." });
    }
}