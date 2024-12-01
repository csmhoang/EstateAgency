using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.BookingEnums;

namespace Core.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        [ForeignKey("Invoice")]
        [MaxLength(36)]
        public string? InvoiceId { get; set; }
        public StatusBooking Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual User? Tenant { get; set; }
        public virtual Invoice? Invoice { get; set; }
        public virtual Lease? Lease { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
