using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Core.Consts;
using static Core.Enums.BookingEnums;

namespace Core.Dtos
{
    public record BookingDetailDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = BookingConst.ErrorEmptyId)]
        public string BookingId { get; set; } = null!;
        [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
        public string RoomId { get; set; } = null!;
        public int NumberOfMonth { get; set; }
        public int NumberOfTenant { get; set; }
        public decimal Price { get; set; }
        public string? RejectionReason { get; set; }
        public StatusBookingDetail? Status { get; set; }

        public virtual BookingDto? Booking { get; set; }
        public virtual RoomDto? Room { get; set; }
    }
}
