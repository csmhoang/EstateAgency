using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record ResetPasswordDto
    {
        [Required(ErrorMessage = UserConst.ErrorEmptyEmail)]
        [EmailAddress(ErrorMessage = UserConst.ErrorFormatEmail)]
        public string Email { get; init; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyNewPassword)]
        public string NewPassword { get; init; } = null!;
        public string Token { get; init; } = null!;
    }
}
