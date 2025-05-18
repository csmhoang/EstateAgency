using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Payment: BaseEntity
{
    [ForeignKey("Invoice")]
    [MaxLength(36)]
    public string? InvoiceId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public PaymentEnums.PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public virtual Invoice? Invoice { get; set; }
}
