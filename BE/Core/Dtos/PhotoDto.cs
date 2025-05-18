using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public record PhotoDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
    public string RoomId { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string PublicId { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
