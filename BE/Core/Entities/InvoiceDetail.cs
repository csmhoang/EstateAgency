using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Entities;

public partial class InvoiceDetail : BaseEntity
{
    [ForeignKey("Invoice")]
    [MaxLength(36)]
    public string? InvoiceId { get; set; }
    [MaxLength(256)]
    public string Detail { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public virtual Invoice? Invoice { get; set; }
}
