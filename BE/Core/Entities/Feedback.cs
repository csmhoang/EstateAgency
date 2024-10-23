using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Feedback
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Tenant")]
        public string? TenantId { get; set; }
        [ForeignKey("Post")]
        public string? PostId { get; set; }
        public int FeedbackCode { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? Tenant { get; set; }
    }
}
