using System.Runtime.Serialization;

namespace Core;

public record LeaseEnums
{
    public enum StatusLease
    {
        [EnumMember(Value = "Pending")]
        Pending = 0,
        [EnumMember(Value = "Active")]
        Active = 1,
        [EnumMember(Value = "Expired")]
        Expired = 2,
        [EnumMember(Value = "Canceled")]
        Canceled = 3,
    }
}
