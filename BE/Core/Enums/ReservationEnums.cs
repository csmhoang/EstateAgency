using System.Runtime.Serialization;

namespace Core;

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
