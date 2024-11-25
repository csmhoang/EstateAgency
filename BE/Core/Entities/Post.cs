using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.PostEnums;

namespace Core.Entities
{
    public partial class Post
    {
        public Post()
        {
            Feedbacks = new HashSet<Feedback>();
            SavePosts = new HashSet<SavePost>();
        }
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Room")]
        [MaxLength(36)]
        public string? RoomId { get; set; }
        [ForeignKey("Landlord")]
        [MaxLength(36)]
        public string? LandlordId { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime AvailableFrom { get; set; }
        public IsAcceptPost IsAccept { get; set; }
        public StatusPost Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual User? Landlord { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<SavePost> SavePosts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
