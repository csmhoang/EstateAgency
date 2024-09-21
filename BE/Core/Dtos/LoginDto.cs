using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record LoginDto
    {
        [Required(ErrorMessage = UserConst.ErrorEmptyUserName)]
        public string UserName { get; init; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyPassword)]
        public string Password { get; init; } = null!;
    }
}
