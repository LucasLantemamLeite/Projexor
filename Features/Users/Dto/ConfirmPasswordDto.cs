using System.ComponentModel.DataAnnotations;

namespace Stokify.Features.Users.Dto;

public record ConfirmPasswordDto
{
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters.")]
    public required string ConfirmPassword { get; init; }
}