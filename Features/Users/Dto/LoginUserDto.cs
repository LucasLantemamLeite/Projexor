using System.ComponentModel.DataAnnotations;

namespace Stokify.Features.Users.Dto;

public sealed record LoginUserDto
{
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [MaxLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters.")]
    public required string Password { get; init; }
}