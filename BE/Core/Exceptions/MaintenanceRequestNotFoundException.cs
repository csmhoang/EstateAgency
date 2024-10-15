using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class MaintenanceRequestNotFoundException : NotFoundException
    {
        public MaintenanceRequestNotFoundException(string id)
            : base($"Không tìm thấy yêu cầu bảo trì có id: {id} trong Database!")
        { }
    }
}
