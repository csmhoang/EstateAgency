using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Payment
    {
        public string Id { get; set; } = null!;
        public string? LeaseId { get; set; }
        public int PaymentCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Lease? Lease { get; set; }
    }
}
