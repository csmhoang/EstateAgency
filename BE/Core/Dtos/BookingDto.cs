using System.ComponentModel.DataAnnotations;
using static Core.Enums.BookingEnums;

namespace Core.Dtos;

public record BookingDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
    public string TenantId { get; set; } = null!;
    [Required(ErrorMessage = InvoiceConst.ErrorEmptyId)]
    public string InvoiceId { get; set; } = null!;
    public StatusBooking? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserDto? Tenant { get; set; }
    public InvoiceDto? Invoice { get; set; }
    public LeaseDto? Lease { get; set; }
    public ICollection<BookingDetailDto>? BookingDetails { get; set; }
}
