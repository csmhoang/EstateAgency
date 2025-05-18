using System.ComponentModel.DataAnnotations;

namespace Core;

public record BookingDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
    public string TenantId { get; set; } = null!;
    [Required(ErrorMessage = InvoiceConst.ErrorEmptyId)]
    public string InvoiceId { get; set; } = null!;
    public BookingEnums.StatusBooking? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserDto? Tenant { get; set; }
    public InvoiceDto? Invoice { get; set; }
    public LeaseDto? Lease { get; set; }
    public ICollection<BookingDetailDto>? BookingDetails { get; set; }
}
