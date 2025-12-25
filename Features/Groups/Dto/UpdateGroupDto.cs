using System.ComponentModel.DataAnnotations;
using Projexor.Shared.Dto;

namespace Projexor.Features.Groups.Dto;

public sealed record UpdateGroupDto : ConfirmPasswordDto
{
    [Required(ErrorMessage = "GroupId is required.")]
    public Guid GroupId { get; init; }

    [MaxLength(100, ErrorMessage = "Name must be between and 100 characters.")]
    public required string? Name { get; init; }

    public bool? Active { get; init; }
}