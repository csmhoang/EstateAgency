using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class SavePost : BaseEntity
{
    [ForeignKey("User")]
    [MaxLength(36)]
    public string? UserId { get; set; }
    [ForeignKey("Post")]
    [MaxLength(36)]
    public string? PostId { get; set; }

    public virtual User? User { get; set; }
    public virtual Post? Post { get; set; }
}
