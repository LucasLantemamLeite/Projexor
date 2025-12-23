using System.ComponentModel.DataAnnotations;
using Projexor.Shared.Dto;

namespace Projexor.Features.Users.Dto;

public sealed record UpdateUserDto : ConfirmPasswordDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public required string Name { get; init; }

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [MaxLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Invalid phone number format.")]
    [MaxLength(15, ErrorMessage = "Phone number most exceed 15 characters.")]
    public required string Phone { get; init; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 30 characters.")]
    public required string Password { get; init; }

    [Required(ErrorMessage = "Active is required.")]
    public bool Active { get; init; }
}