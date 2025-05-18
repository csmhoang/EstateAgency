using System.ComponentModel.DataAnnotations;
using static Core.Enums.PaymentEnums;

namespace Core.Dtos;

public record PaymentDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = PaymentConst.ErrorEmptyId)]
    public string InvoiceId { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
