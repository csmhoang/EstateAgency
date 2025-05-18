using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Invoice: BaseEntity
{
    public Invoice()
    {
        InvoiceDetails = new HashSet<InvoiceDetail>();
    }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceEnums.StatusInvoice Status { get; set; }

    public virtual Booking? Booking { get; set; }
    public virtual Payment? Payment { get; set; }
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
}
