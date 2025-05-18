using System.ComponentModel.DataAnnotations;
namespace Core.Dtos;

public record CartDetailDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = CartConst.ErrorEmptyId)]
    public string CartId { get; set; } = null!;
    [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
    public string RoomId { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public int NumberOfMonth { get; set; }
    public int NumberOfTenant { get; set; }
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public CartDto? Cart { get; set; }
    public RoomDto? Room { get; set; }
}
