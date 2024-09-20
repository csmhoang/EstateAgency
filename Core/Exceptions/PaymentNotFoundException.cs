using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public sealed class PaymentNotFoundException : NotFoundException
    {
        public PaymentNotFoundException(string? id)
            : base($"Không tìm thấy thanh toán có id: {id} trong Database!")
        { }
    }
}
