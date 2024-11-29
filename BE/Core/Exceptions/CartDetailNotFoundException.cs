using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class CartDetailNotFoundException : NotFoundException
    {
        public CartDetailNotFoundException(string id)
            : base($"Không tìm thấy chi tiết giỏ phòng có id: {id} trong hệ thống!")
        { }
    }
}
