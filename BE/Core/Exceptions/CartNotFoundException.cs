using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class CartNotFoundException : NotFoundException
    {
        public CartNotFoundException(string id)
            : base($"Không tìm thấy giỏ phòng có id: {id} trong hệ thống!")
        { }
    }
}
