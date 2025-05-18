using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public partial class UserRole : IdentityUserRole<string>
{
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}
