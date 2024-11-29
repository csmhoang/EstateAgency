using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.LeaseEnums;

namespace Core.Entities
{
    public partial class LeaseDetail
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Booking")]
        [MaxLength(36)]
        public string? BookingId { get; set; }
        [ForeignKey("Lease")]
        [MaxLength(36)]
        public string? LeaseId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual Booking? Booking { get; set; }
        public virtual Lease? Lease { get; set; }
    }
}
