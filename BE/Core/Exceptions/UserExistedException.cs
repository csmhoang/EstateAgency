using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class UserExistedException : BadRequestException
    {
        public UserExistedException(string username)
            : base($"Người dùng với Username: {username} đã tồn tại!")
        { }
    }
}
