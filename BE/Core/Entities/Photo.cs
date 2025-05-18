using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Photo: BaseEntity
{
    [ForeignKey("Room")]
    [MaxLength(36)]
    public string RoomId { get; set; } = null!;
    [MaxLength(256)]
    public string Url { get; set; } = null!;
    [MaxLength(256)]
    public string PublicId { get; set; } = null!;

    public virtual Room? Room { get; set; }
}
