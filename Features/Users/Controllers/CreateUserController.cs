using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projexor.Data.Context;
using Projexor.Features.Users.Auth;
using Projexor.Features.Users.Dto;
using Projexor.Features.Users.Models;
using Projexor.Services;

namespace Projexor.Features.Users.Controllers;

[ApiController]
[Route("v1/user/create")]
[Tags("Create")]
public sealed class CreateUserController(AppDbContext context) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ExecuteAsync([FromBody] CreateUserDto createDto, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (await context.Users.AnyAsync(x => x.Email == createDto.Email, cancellationToken))
            return Conflict(new { message = "This email address is already registered." });

        if (await context.Users.AnyAsync(x => x.Phone == createDto.Phone, cancellationToken))
            return Conflict(new { message = "This phone number is already registered." });

        var user = new User(
            name: createDto.Name,
            email: createDto.Email,
            phone: createDto.Phone,
            password: Hasher.GenerateHash(createDto.Password),
            isSuperAdmin: false
        );

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        return Created("", new { message = "User created successfully!", token = user.GenerateToken() });
    }
}