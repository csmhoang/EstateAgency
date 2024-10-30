using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class PhotoNotFoundException: NotFoundException
    {
        public PhotoNotFoundException(string id)
            : base($"Không tìm thấy ảnh có id: {id} trong hệ thống!")
        { }
    }
}
