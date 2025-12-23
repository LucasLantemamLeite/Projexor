using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projexor.Data.Context;
using Projexor.Features.Users.Auth;
using Projexor.Features.Users.Dto;

namespace Projexor.Features.Users.Controllers;

[ApiController]
[Route("v1/user/delete")]
[Tags("Delete")]
public sealed class DeleteUserController(AppDbContext context) : ControllerBase
{
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> ExecuteAsync([FromBody] ConfirmPasswordDto passwordDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out var userId))
            return Unauthorized(new { message = "Invalid user identifier." });

        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null || !Hasher.VerifyHash(user.Password, passwordDto.ConfirmPassword))
            return Unauthorized(new { message = "Invalid credentials." });

        context.Users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}