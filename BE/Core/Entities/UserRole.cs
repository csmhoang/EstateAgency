using Microsoft.AspNetCore.Identity;

namespace Core;

public partial class UserRole : IdentityUserRole<string>
{
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}
