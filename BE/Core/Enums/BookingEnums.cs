using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record BookingEnums
    {
        public enum StatusBooking
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "Accepted")]
            Accepted = 1,
            [EnumMember(Value = "Rejected")]
            Rejected = 2,
            [EnumMember(Value = "Canceled")]
            Canceled = 3
        }
    }
}
