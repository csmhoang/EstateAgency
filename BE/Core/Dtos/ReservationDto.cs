using System.ComponentModel.DataAnnotations;

namespace Core;

public record ReservationDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
    public string TenantId { get; set; } = null!;
    [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
    public string RoomId { get; set; } = null!;
    public string? Note { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime ReservationDate { get; set; }
    public int ReservationHour { get; set; }
    public int ReservationMinute { get; set; }
    public ReservationEnums.StatusReservation? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public RoomDto? Room { get; set; }
    public UserDto? Tenant { get; set; }
}
