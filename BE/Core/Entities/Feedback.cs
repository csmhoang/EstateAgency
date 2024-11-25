using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Feedback
    {
        public Feedback()
        {
            Replies = new HashSet<Feedback>();
        }
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        [ForeignKey("Post")]
        [MaxLength(36)]
        public string? PostId { get; set; }
        [ForeignKey("Reply")]
        [MaxLength(36)]
        public string? ReplyId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Post? Post { get; set; }
        public virtual User? Tenant { get; set; }
        public virtual Feedback? Reply { get; set; }
        public virtual ICollection<Feedback> Replies { get; set; }
    }
}
