using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.UserEnums;

namespace Core.Dtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage = UserConst.ErrorEmptyEmail)]
        [EmailAddress(ErrorMessage = UserConst.ErrorFormatEmail)]
        public string Email { get; init; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyFullName)]
        public string FullName { get; set; } = null!;
        public string? PhoneNumber { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public Gender Gender { get; init; }
        [Required(ErrorMessage = UserConst.ErrorEmptyPassword)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = UserConst.ErrorLengthPassword)]
        public string Password { get; init; } = null!;
        public string? Address { get; init; }
        public IEnumerable<string>? Roles { get; init; }
    }
}
