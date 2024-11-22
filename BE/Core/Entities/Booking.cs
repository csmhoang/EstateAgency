using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.BookingEnums;

namespace Core.Entities
{
    public partial class Booking
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Post")]
        public string? PostId { get; set; }
        [ForeignKey("Tenant")]
        public string? TenantId { get; set; }
        public DateTime IntendedIntoDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? RejectionReason { get; set; }
        public string? Note { get; set; }
        public StatusBooking Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? Tenant { get; set; }
        public virtual Post? Post { get; set; }
    }
}
