using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projexor.Data.Context;
using Projexor.Features.Users.Auth;
using Projexor.Features.Users.Dto;

namespace Projexor.Features.Users.Controllers;

[ApiController]
[Route("v1/user/update")]
[Tags("Update")]
public sealed class UpdateUserController(AppDbContext context) : ControllerBase
{
    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> ExecuteAsync([FromBody] UpdateUserDto updateDto, CancellationToken cancellationToken = default)
    {
        if (updateDto.GetType().GetProperties().Where(p => p.Name != "ConfirmPassword").All(p => p.GetValue(updateDto) is null))
            return NoContent();

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(id, out var userId))
            return Unauthorized(new { message = "Invalid user identifier." });

        var user = await context.Users.SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (user is null || !Hasher.VerifyHash(user.Password, updateDto.ConfirmPassword))
            return Unauthorized(new { message = "Invalid credentials." });

        if (updateDto.Email is not null && await context.Users.AnyAsync(x => x.Email == updateDto.Email && x.Id != userId, cancellationToken: cancellationToken))
            return Conflict(new { message = "This email address is already registered." });

        if (updateDto.Phone is not null && await context.Users.AnyAsync(x => x.Phone == updateDto.Phone && x.Id != userId, cancellationToken: cancellationToken))
            return Conflict(new { message = "This phone number is already registered." });

        user.ChangeName(updateDto.Name);
        user.ChangeEmail(updateDto.Email);
        user.ChangePhone(updateDto.Phone);
        user.ChangeActive(updateDto.Active);

        context.Users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        return Ok(new { message = "User successfully updated." });
    }
}
