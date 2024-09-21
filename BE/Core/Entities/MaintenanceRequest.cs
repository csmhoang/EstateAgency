using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class MaintenanceRequest
    {
        public string Id { get; set; } = null!;
        public string? LeaseId { get; set; }
        public int RequestCode { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? RequestDate { get; set; }
        public string? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Lease? Lease { get; set; }
    }
}
