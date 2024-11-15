using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.MaintenanceRequestEnums;

namespace Core.Entities
{
    public partial class MaintenanceRequest
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Lease")]
        public string? LeaseId { get; set; }
        [ForeignKey("Invoice")]
        public string? InvoiceId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public StatusMaintenanceRequest Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Lease? Lease { get; set; }
        public virtual Invoice? Invoice { get; set; }
    }
}
