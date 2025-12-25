using System.ComponentModel.DataAnnotations;
using Projexor.Shared.Dto;

namespace Projexor.Features.Users.Dto;

public sealed record UpdateUserDto : ConfirmPasswordDto
{
    [MaxLength(100, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public string? Name { get; init; }

    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [MaxLength(255, ErrorMessage = "Email must not exceed 255 characters.")]
    public string? Email { get; init; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    [MaxLength(15, ErrorMessage = "Phone number most exceed 15 characters.")]
    public string? Phone { get; init; }

    public bool? Active { get; init; }
}