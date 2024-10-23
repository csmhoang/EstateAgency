using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record LeaseDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
        public string? TenantId { get; set; }
        [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
        public string? RoomId { get; set; }
        public int LeaseCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? SignedOnline { get; set; }
        public DateTime? SignedDate { get; set; }
        public string? Status { get; set; }
    }
}
