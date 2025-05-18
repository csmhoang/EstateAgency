using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Reservation : BaseEntity
{
    [ForeignKey("Tenant")]
    [MaxLength(36)]
    public string? TenantId { get; set; }
    [ForeignKey("Room")]
    [MaxLength(36)]
    public string? RoomId { get; set; }
    public string? Note { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime ReservationDate { get; set; }
    public ReservationEnums.StatusReservation Status { get; set; }

    public virtual Room? Room { get; set; }
    public virtual User? Tenant { get; set; }
}
