using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class UserRole : IdentityUserRole<string>
    {
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
}
