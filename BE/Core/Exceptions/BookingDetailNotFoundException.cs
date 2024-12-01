using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class BookingDetailNotFoundException : NotFoundException
    {
        public BookingDetailNotFoundException(string id)
            : base($"Không tìm thấy chi tiết đơn đặt phòng có id: {id} trong hệ thống!")
        { }
    }
}
