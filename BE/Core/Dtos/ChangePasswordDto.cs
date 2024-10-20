﻿using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record ChangePasswordDto
    {
        [Required(ErrorMessage = UserConst.ErrorEmptyEmail)]
        public string Email { get; init; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyPassword)]
        public string Password { get; init; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyNewPassword)]
        public string NewPassword { get; init; } = null!;
    }
}
