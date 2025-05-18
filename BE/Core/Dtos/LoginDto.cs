using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public record LoginDto
{
    [Required(ErrorMessage = UserConst.ErrorEmptyEmail)]
    public string Email { get; init; } = null!;
    [Required(ErrorMessage = UserConst.ErrorEmptyPassword)]
    public string Password { get; init; } = null!;
}
