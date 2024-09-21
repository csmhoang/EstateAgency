using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class InvoiceNotFoundException : NotFoundException
    {
        public InvoiceNotFoundException(string? id)
            : base($"Không tìm thấy hóa đơn có id: {id} trong Database!")
        { }
    }
}
