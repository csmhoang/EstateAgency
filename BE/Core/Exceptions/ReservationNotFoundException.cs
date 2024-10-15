using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class ReservationNotFoundException : NotFoundException
    {
        public ReservationNotFoundException(string id)
            : base($"Không tìm thấy lịch đặt có id: {id} trong Database!")
        { }
    }
}
