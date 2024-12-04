using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record LeaseEnums
    {
        public enum StatusLease
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "Active")]
            Confirmed = 1,
            [EnumMember(Value = "Active")]
            Active = 2,
            [EnumMember(Value = "Expired")]
            Expired = 3,
            [EnumMember(Value = "Rejected")]
            Canceled = 4,
        }
    }
}
