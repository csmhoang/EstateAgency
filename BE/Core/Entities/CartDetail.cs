using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class CartDetail : BaseEntity
    {
        [ForeignKey("Cart")]
        [MaxLength(36)]
        public string CartId { get; set; } = null!;
        [ForeignKey("Room")]
        [MaxLength(36)]
        public string? RoomId { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        public int NumberOfMonth { get; set; }
        public int NumberOfTenant { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual Room? Room { get; set; }
    }
}
