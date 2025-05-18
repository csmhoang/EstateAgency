using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.BookingEnums;

namespace Core.Entities;

public partial class BookingDetail : BaseEntity
{
    [ForeignKey("Booking")]
    [MaxLength(36)]
    public string BookingId { get; set; } = null!;
    [ForeignKey("Room")]
    [MaxLength(36)]
    public string RoomId { get; set; } = null!;
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public int NumberOfMonth { get; set; }
    public int NumberOfTenant { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public string? RejectionReason { get; set; }
    public StatusBookingDetail Status { get; set; }

    public virtual Booking? Booking { get; set; }
    public virtual Room? Room { get; set; }
}
