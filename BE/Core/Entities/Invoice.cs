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
            MaintenanceRequests = new HashSet<MaintenanceRequest>();
        }
        public string Id { get; set; } = null!;
        [ForeignKey("Lease")]
        public string? LeaseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusInvoice? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Lease? Lease { get; set; }
        public virtual Payment? Payment { get; set; }

        public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
    }
}
