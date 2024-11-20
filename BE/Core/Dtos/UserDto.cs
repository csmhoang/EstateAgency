using Core.Entities;
using static Core.Enums.UserEnums;

namespace Core.Dtos
{
    public record UserDto
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string? PublicId { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = null!;
        public string? Description { get; set; }
        public int NumberOfFollowers { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<RoomDto>? Rooms { get; set; }
        public ICollection<PostDto>? Posts { get; set; }
        public ICollection<UserRoleDto>? UserRoles { get; set; }
        public ICollection<Follow>? Followers { get; set; }
        public ICollection<Follow>? Followees { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<SavePost>? SavePosts { get; set; }

    }
}
