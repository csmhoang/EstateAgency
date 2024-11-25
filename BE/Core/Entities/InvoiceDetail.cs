using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Entities
{
    public partial class InvoiceDetail
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Invoice")]
        [MaxLength(36)]
        public string? InvoiceId { get; set; }
        [MaxLength(256)]
        public string? Name { get; set; }
        public string? Detail { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual Invoice? Invoice { get; set; }
    }
}
