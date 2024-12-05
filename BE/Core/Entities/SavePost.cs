using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class SavePost : BaseEntity
    {
        [ForeignKey("User")]
        [MaxLength(36)]
        public string? UserId { get; set; }
        [ForeignKey("Post")]
        [MaxLength(36)]
        public string? PostId { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
