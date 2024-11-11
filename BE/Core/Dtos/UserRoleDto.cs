using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record UserRoleDto
    {
        public UserDto? User { get; set; }
        public RoleDto? Role { get; set; }
    }
}
