using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.BookingEnums;

namespace Core.Entities
{
    public partial class Booking
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Post")]
        public string? PostId { get; set; }
        [ForeignKey("Landlord")]
        public string? LandlordId { get; set; }
        public DateTime IntendedIntoDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? Note { get; set; }
        public StatusBooking Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? Landlord { get; set; }
        public virtual Post? Post { get; set; }
    }
}
