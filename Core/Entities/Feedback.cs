using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Feedback
    {
        public string Id { get; set; } = null!;
        public string? TenantId { get; set; }
        public string? RoomId { get; set; }
        public int FeedbackCode { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Room? Room { get; set; }
        public virtual User? Tenant { get; set; }
    }
}
