using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.BookingEnums;

namespace Core.Entities
{
    public partial class Booking
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Room")]
        [MaxLength(36)]
        public string? RoomId { get; set; }
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        [ForeignKey("Invoice")]
        [MaxLength(36)]
        public string? InvoiceId { get; set; }
        [Column(TypeName = "date")]
        public DateTime IntendedIntoDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? Note { get; set; }
        public string? RejectionReason { get; set; }
        public StatusBooking Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual User? Tenant { get; set; }
        public virtual Room? Room { get; set; }
        public virtual Invoice? Invoice { get; set; }
    }
}
