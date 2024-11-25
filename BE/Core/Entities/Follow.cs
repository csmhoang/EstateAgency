using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Follow
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Follower")]
        [MaxLength(36)]
        public string? FollowerId { get; set; }
        [ForeignKey("Followee")]
        [MaxLength(36)]
        public string? FolloweeId { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual User? Follower { get; set; }
        public virtual User? Followee { get; set; }
    }
}
