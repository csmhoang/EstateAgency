using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record ReservationEnums
    {
        public enum StatusReservation
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "Confirmed")]
            Confirmed = 1,
            [EnumMember(Value = "Rejected")]
            Rejected = 2,
            [EnumMember(Value = "Canceled")]
            Canceled = 3
        }
    }
}
