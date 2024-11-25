using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Message
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Sender")]
        [MaxLength(36)]
        public string? SenderId { get; set; }
        [ForeignKey("Receiver")]
        [MaxLength(36)]
        public string? ReceiverId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
