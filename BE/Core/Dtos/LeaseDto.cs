using System.ComponentModel.DataAnnotations;

namespace Core;

public record LeaseDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = BookingConst.ErrorEmptyId)]
    public string BookingId { get; set; } = null!;
    public string Lessor { get; set; } = null!;
    public string Lessee { get; set; } = null!;
    public string? Terms { get; set; }
    public DateTime? SignedDate { get; set; }
    public LeaseEnums.StatusLease? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<LeaseDetailDto>? LeaseDetails { get; set; }
}
