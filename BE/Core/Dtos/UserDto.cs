using static Core.Enums.UserEnums;

namespace Core.Dtos
{
    public record UserDto
    {
        public Guid? Id { get; set; }
        public int UserCode { get; set; }
        public string FullName { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public string PublicId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
