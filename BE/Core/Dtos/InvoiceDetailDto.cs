namespace Core.Dtos;

public record InvoiceDetailDto
{
    public string? Id { get; set; }
    public string? InvoiceId { get; set; }
    public string Detail { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
