using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
            Leases = new HashSet<Lease>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            Reservations = new HashSet<Reservation>();
            Rooms = new HashSet<Room>();
        }
        public int UserCode { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// 0 - Nam
        /// 1 - Nữ
        /// 3 - Không xác định
        /// </summary>
        public int Gender { get; set; }
        public string? Address { get; set; }
        public string? RefreshToken { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PublicId { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
