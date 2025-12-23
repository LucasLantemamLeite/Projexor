using System.ComponentModel.DataAnnotations;

namespace Projexor.Features.Groups.Dto;

public sealed record CreateGroupDto
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name must be between and 100 characters.")]
    public required string Name { get; init; }
}