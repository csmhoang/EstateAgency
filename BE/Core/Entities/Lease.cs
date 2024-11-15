using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.LeaseEnums;

namespace Core.Entities
{
    public partial class Lease
    {
        public Lease()
        {
            Invoices = new HashSet<Invoice>();
            MaintenanceRequests = new HashSet<MaintenanceRequest>();
            Payments = new HashSet<Payment>();
        }

        public string Id { get; set; } = null!;
        [ForeignKey("Tenant")]
        public string? TenantId { get; set; }
        [ForeignKey("Room")]
        public string? RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? SignedOnline { get; set; }
        public DateTime SignedDate { get; set; }
        public StatusLeasse Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Room? Room { get; set; }
        public virtual User? Tenant { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
