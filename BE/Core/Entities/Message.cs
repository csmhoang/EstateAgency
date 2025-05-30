﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Message: BaseEntity
{
    [ForeignKey("Sender")]
    [MaxLength(36)]
    public string? SenderId { get; set; }
    [ForeignKey("Receiver")]
    [MaxLength(36)]
    public string? ReceiverId { get; set; }
    [ForeignKey("Conversation")]
    [MaxLength(36)]
    public string? ConversationId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime SentAt { get; set; } = DateTime.Now;

    public virtual User? Receiver { get; set; }
    public virtual User? Sender { get; set; }
    public virtual Conversation? Conversation { get; set; }
}
