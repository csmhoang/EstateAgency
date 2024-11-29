using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.LeaseEnums;

namespace Core.Entities
{
    public partial class Lease
    {
        public Lease()
        {
            LeaseDetails = new HashSet<LeaseDetail>();
        }

        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        public string? Terms { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime SignedDate { get; set; }
        public bool IsConfirm { get; set; }
        public StatusLeasse Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual User? Tenant { get; set; }
        public virtual ICollection<LeaseDetail> LeaseDetails { get; set; }
    }
}
