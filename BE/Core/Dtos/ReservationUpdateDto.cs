namespace Core;

public record ReservationUpdateDto
{
    public string? Note { get; set; }
    public DateTime ReservationDate { get; set; }
    public int ReservationHour { get; set; }
    public int ReservationMinute { get; set; }
}
