using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public partial class SavePostDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyUserId)]
    public string UserId { get; set; } = null!;
    [Required(ErrorMessage = PostConst.ErrorEmptyId)]
    public string PostId { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual UserDto? User { get; set; }
    public virtual PostDto? Post { get; set; }
}
