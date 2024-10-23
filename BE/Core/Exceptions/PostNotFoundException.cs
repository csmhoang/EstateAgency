using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException(string id)
            : base($"Không tìm thấy bài đăng có id: {id} trong Database!")
        { }
    }
}
