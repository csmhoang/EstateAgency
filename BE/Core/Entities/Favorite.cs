using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class Favorite
    {
        public Favorite()
        {
            PostSaves = new HashSet<SavePost>();
        }
        public string Id { get; set; } = null!;

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<SavePost> PostSaves { get; set; }
    }
}
