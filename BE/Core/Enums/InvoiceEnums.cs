using System.Runtime.Serialization;

namespace Core;

public record InvoiceEnums
{
    public enum StatusInvoice
    {
        [EnumMember(Value = "Pending")]
        Pending = 0,
        [EnumMember(Value = "Paid")]
        Paid = 1,
        [EnumMember(Value = "Overdue")]
        Overdue = 2,
        [EnumMember(Value = "Cancelled")]
        Cancelled = 3
    }

}
