using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record InvoiceEnums
    {
        public enum StatusInvoice
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "Paid")]
            Paid = 1,
            [EnumMember(Value = "Overdue")]
            Overdue = 2
        }

    }
}
