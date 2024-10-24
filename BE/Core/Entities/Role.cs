using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public partial class Role : IdentityRole
    {
        public Role()
        {
            UserRoles = new HashSet<IdentityUserRole<string>>();
        }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
