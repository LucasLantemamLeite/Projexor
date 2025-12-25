using System.ComponentModel.DataAnnotations;
using Projexor.Shared.Dto;

namespace Projexor.Features.Groups.Dto;

public sealed record DeleteGroupDto : ConfirmPasswordDto
{
    [Required(ErrorMessage = "GroupId is required.")]
    public Guid GroupId { get; set; }
}