using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(string? id)
            : base($"Không tìm thấy phòng có id: {id} trong Database!")
        { }
    }
}
