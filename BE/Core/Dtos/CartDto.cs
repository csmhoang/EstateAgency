using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public record CartDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
    public string? TenantId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserDto? Tenant { get; set; }
    public ICollection<CartDetailDto>? CartDetails { get; set; }
}
