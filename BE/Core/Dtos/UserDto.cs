﻿namespace Core;

public record UserDto
{
    public string? Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public string? PublicId { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public UserEnums.Gender Gender { get; set; }
    public string Address { get; set; } = null!;
    public string? Description { get; set; }
    public int NumberOfFollowers { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<RoomDto>? Rooms { get; set; }
    public ICollection<PostDto>? Posts { get; set; }
    public ICollection<UserRoleDto>? UserRoles { get; set; }
    public ICollection<FollowDto>? Followers { get; set; }
    public ICollection<FollowDto>? Followees { get; set; }
    public ICollection<ReservationDto>? Reservations { get; set; }
    public ICollection<BookingDto>? Bookings { get; set; }
    public ICollection<SavePostDto>? SavePosts { get; set; }
    public CartDto? Cart { get; set; }
}
