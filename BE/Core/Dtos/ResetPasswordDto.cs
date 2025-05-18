using System.ComponentModel.DataAnnotations;

namespace Core;

public record ResetPasswordDto
{
    [Required(ErrorMessage = UserConst.ErrorEmptyEmail)]
    [EmailAddress(ErrorMessage = UserConst.ErrorFormatEmail)]
    public string Email { get; init; } = null!;
    [Required(ErrorMessage = UserConst.ErrorEmptyNewPassword)]
    public string NewPassword { get; init; } = null!;
    public string Token { get; init; } = null!;
}
