using System.Runtime.Serialization;

namespace Core;

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
