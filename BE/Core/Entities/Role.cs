﻿using Microsoft.AspNetCore.Identity;

namespace Core;

public partial class Role : IdentityRole
{
    public Role()
    {
        UserRoles = new HashSet<UserRole>();
    }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}
