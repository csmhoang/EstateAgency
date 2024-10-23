using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Message
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Sender")]
        public string? SenderId { get; set; }
        [ForeignKey("Receiver")]
        public string? ReceiverId { get; set; }
        public int MessageCode { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? SentAt { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
