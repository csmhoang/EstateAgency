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
        [ForeignKey("Booking")]
        [MaxLength(36)]
        public string? BookingId { get; set; }
        [MaxLength(100)]
        public string Lessor { get; set; } = null!;
        [MaxLength(100)]
        public string Lessee { get; set; } = null!;
        public string Terms { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SignedDate { get; set; }
        public StatusLeasse Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual User? Tenant { get; set; }
        public virtual Booking? Booking { get; set; }
        public virtual ICollection<LeaseDetail> LeaseDetails { get; set; }
    }
}
