using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Follow : BaseEntity
    {

        [ForeignKey("Follower")]
        [MaxLength(36)]
        public string? FollowerId { get; set; }
        [ForeignKey("Followee")]
        [MaxLength(36)]
        public string? FolloweeId { get; set; }
        public virtual User? Follower { get; set; }
        public virtual User? Followee { get; set; }
    }
}
