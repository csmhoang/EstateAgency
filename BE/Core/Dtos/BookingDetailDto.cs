﻿using System.ComponentModel.DataAnnotations;

namespace Core;

public record BookingDetailDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = BookingConst.ErrorEmptyId)]
    public string BookingId { get; set; } = null!;
    [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
    public string RoomId { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int NumberOfMonth { get; set; }
    public int NumberOfTenant { get; set; }
    public decimal Price { get; set; }
    public string? RejectionReason { get; set; }
    public BookingEnums.StatusBookingDetail? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual BookingDto? Booking { get; set; }
    public virtual RoomDto? Room { get; set; }
}
