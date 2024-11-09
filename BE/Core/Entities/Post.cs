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

        public string Id { get; set; } = null!;
        [ForeignKey("Room")]
        public string? RoomId { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AvailableFrom { get; set; }
        public StatusPost Status { get; set; }
        public IsAcceptPost IsAccept { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Room? Room { get; set; }
        public virtual ICollection<SavePost> SavePosts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
