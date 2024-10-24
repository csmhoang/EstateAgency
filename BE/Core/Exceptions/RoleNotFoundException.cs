using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string role)
            : base($"Không tồn tại role: {role} trong hệ thống!")
        { }
    }
}
