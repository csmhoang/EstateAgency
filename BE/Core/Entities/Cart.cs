using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.BookingEnums;

namespace Core.Entities
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        public virtual User? Tenant { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
