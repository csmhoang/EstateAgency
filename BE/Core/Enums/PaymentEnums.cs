using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record PaymentEnums
    {
        public enum StatusPayment
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "Completed")]
            Completed = 1,
            [EnumMember(Value = "Failed")]
            Failed = 2
        }

        public enum PaymentMethod
        {
            [EnumMember(Value = "CreditCard")]
            CreditCard = 0,
            [EnumMember(Value = "Cash")]
            Cash = 1,
        }
    }
}
