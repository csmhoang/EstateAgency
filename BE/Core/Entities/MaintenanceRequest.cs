using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.MaintenanceRequestEnums;

namespace Core.Entities
{
    public partial class MaintenanceRequest : BaseEntity
    {
        public MaintenanceRequest()
        {
            MaintenanceImages = new HashSet<MaintenanceImage>();
        }
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        [ForeignKey("Invoice")]
        [MaxLength(36)]
        public string? InvoiceId { get; set; }
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? EstimateCost { get; set; }
        [Column(TypeName = "date")]
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public StatusMaintenanceRequest Status { get; set; }

        public virtual Invoice? Invoice { get; set; }
        public virtual User? Tenant { get; set; }
        public virtual ICollection<MaintenanceImage> MaintenanceImages { get; set; }
    }
}
