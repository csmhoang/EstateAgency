using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public partial class Participant : BaseEntity
{
    [ForeignKey("User")]
    [MaxLength(36)]
    public string? UserId { get; set; }
    [ForeignKey("Conversation")]
    [MaxLength(36)]
    public string? ConversationId { get; set; }
    public virtual User? User { get; set; }
    public virtual Conversation? Conversation { get; set; }
}
