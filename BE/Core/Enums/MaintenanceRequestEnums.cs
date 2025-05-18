using System.Runtime.Serialization;

namespace Core;

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
