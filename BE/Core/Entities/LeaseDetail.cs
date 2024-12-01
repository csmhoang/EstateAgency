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
        [ForeignKey("Room")]
        [MaxLength(36)]
        public string RoomId { get; set; } = null!;
        [ForeignKey("Lease")]
        [MaxLength(36)]
        public string? LeaseId { get; set; }
        public int NumberOfMonth { get; set; }
        public int NumberOfTenant { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public virtual Room? Room { get; set; }
        public virtual Lease? Lease { get; set; }
    }
}
