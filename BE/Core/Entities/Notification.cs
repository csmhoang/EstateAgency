using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.NotificationEnums;

namespace Core.Entities;

public partial class Notification : BaseEntity
{
    [ForeignKey("Receiver")]
    [MaxLength(36)]
    public string? ReceiverId { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public StatusNotification Status { get; set; }

    public virtual User? Receiver { get; set; }
}
