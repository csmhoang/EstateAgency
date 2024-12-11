using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.UserEnums;

namespace Core.Entities
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
            Followers = new HashSet<Follow>();
            Followees = new HashSet<Follow>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            Reservations = new HashSet<Reservation>();
            Bookings = new HashSet<Booking>();
            Rooms = new HashSet<Room>();
            Posts = new HashSet<Post>();
            UserRoles = new HashSet<UserRole>();
            SavePosts = new HashSet<SavePost>();
            Participants = new HashSet<Participant>();
        }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        [MaxLength(256)]
        public string Address { get; set; } = null!;
        public string? RefreshToken { get; set; }
        [MaxLength(256)]
        public string? AvatarUrl { get; set; }
        [MaxLength(256)]
        public string? PublicId { get; set; }
        public string? Description { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual Cart? Cart { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<SavePost> SavePosts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Follow> Followers { get; set; }
        public virtual ICollection<Follow> Followees { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
