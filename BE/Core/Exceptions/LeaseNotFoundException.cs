using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class LeaseNotFoundException : NotFoundException
    {
        public LeaseNotFoundException(string id)
            : base($"Không tìm thấy hợp đồng có id: {id} trong Database!")
        { }
    }
}
