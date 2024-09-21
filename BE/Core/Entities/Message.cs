using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Message
    {
        public string Id { get; set; } = null!;
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public int MessageCode { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? SentAt { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
