using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public record FollowDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyFollowerId)]
    public string FollowerId { get; set; } = null!;
    [Required(ErrorMessage = UserConst.ErrorEmptyFolloweeId)]
    public string FolloweeId { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserDto? Follower { get; set; }
    public UserDto? Followee { get; set; }
}
