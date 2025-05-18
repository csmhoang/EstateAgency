namespace Core;

public record RoomUpdateDto
{
    public string Name { get; set; } = null!;
    public RoomEnums.Category Category { get; set; }
    public string Address { get; set; } = null!;
    public string Ward { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string District { get; set; } = null!;
    public int Bedroom { get; set; }
    public int Bathroom { get; set; }
    public int Toilet { get; set; }
    public RoomEnums.Interior Interior { get; set; }
    public decimal Area { get; set; }
    public decimal Price { get; set; }
}
