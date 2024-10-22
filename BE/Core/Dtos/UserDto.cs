using Core.Enums;

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
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? GenderName
        {
            get
            {
                return Gender switch
                {
                    (int)UserEnums.Gender.MALE => "Nam",
                    (int)UserEnums.Gender.FEMALE => "Nữ",
                    (int)UserEnums.Gender.OTHER => "Không xác định",
                    _ => "Không xác định",
                };
            }
            set { }
        }
    }
}
