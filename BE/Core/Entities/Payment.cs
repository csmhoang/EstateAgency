using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.PaymentEnums;

namespace Core.Entities
{
    public partial class Payment
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Invoice")]
        [MaxLength(36)]
        public string? InvoiceId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public virtual Invoice? Invoice { get; set; }
    }
}
