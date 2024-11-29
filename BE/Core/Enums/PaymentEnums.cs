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

        public enum PaymentMethod
        {
            [EnumMember(Value = "CreditCard")]
            CreditCard = 0,
            [EnumMember(Value = "Cash")]
            Cash = 1,
        }
    }
}
