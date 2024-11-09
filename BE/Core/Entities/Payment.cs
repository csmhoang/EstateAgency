using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.PaymentEnums;

namespace Core.Entities
{
    public partial class Payment
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Lease")]
        public string? LeaseId { get; set; }
        [ForeignKey("Invoice")]
        public string? InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public StatusPayment Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual Lease? Lease { get; set; }
    }
}
