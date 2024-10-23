using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public record MaintenanceRequestEnums
    {
        public enum StatusMaintenanceRequest
        {
            [EnumMember(Value = "Pending")]
            Pending = 0,
            [EnumMember(Value = "InProgress")]
            InProgress = 1,
            [EnumMember(Value = "Completed")]
            Completed = 2,
            [EnumMember(Value = "Rejected")]
            Rejected = 3
        }
    }
}
