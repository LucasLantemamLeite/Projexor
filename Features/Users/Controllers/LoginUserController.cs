using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokify.Data.Context;
using Stokify.Features.Users.Auth;
using Stokify.Features.Users.Dto;
using Stokify.Services;

namespace Stokify.Features.Users.Controllers;

[ApiController]
[Route("v1/user/auth")]
[Tags("Auth")]
public sealed class LoginUserController(AppDbContext context) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ExecuteAsync([FromBody] LoginUserDto loginDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email, cancellationToken: cancellationToken);

        if (user is null || !Hasher.VerifyHash(user.Password, loginDto.Password))
            return Unauthorized(new { message = "Invalid credentials." });

        return Ok(new { message = "Login successfully.", token = user.GenerateToken() });
    }
}