using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.InvoiceEnums;

namespace Core.Entities
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }
        public StatusInvoice Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual Booking? Booking { get; set; }
        public virtual MaintenanceRequest? MaintenanceRequest { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
