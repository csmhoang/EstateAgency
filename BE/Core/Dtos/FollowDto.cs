using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record FollowDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyFollowerId)]
        public string FollowerId { get; set; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyFolloweeId)]
        public string FolloweeId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
