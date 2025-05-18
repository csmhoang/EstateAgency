namespace Core.Dtos;

public record PostUpdateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime AvailableFrom { get; set; }
}
