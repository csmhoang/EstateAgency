using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Notification : BaseEntity
{
    [ForeignKey("Receiver")]
    [MaxLength(36)]
    public string? ReceiverId { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public NotificationEnums.StatusNotification Status { get; set; }

    public virtual User? Receiver { get; set; }
}
