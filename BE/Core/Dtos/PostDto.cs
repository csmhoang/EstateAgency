using System.ComponentModel.DataAnnotations;
using static Core.Enums.PostEnums;

namespace Core.Dtos;

public record PostDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
    public string RoomId { get; set; } = null!;
    [Required(ErrorMessage = UserConst.ErrorEmptyLandlordId)]
    public string LandlordId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime AvailableFrom { get; set; }
    public IsAcceptPost? IsAccept { get; set; }
    public bool IsHide { get; set; }
    public StatusPost? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public RoomDto? Room { get; set; }
    public UserDto? Landlord { get; set; }
}
