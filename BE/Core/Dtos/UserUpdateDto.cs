namespace Core;

public record UserUpdateDto
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public UserEnums.Gender Gender { get; set; }
    public string Address { get; set; } = null!;
    public string? Description { get; set; }
}
