using Core.Consts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public partial class SavePostDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyUserId)]
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = PostConst.ErrorEmptyId)]
        public string PostId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
