namespace Core;

public record InvoiceDto
{
    public string? Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceEnums.StatusInvoice? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<InvoiceDetailDto>? InvoiceDetails { get; set; }
    public BookingDto? Booking { get; set; }
    public PaymentDto? Payment { get; set; }
}
