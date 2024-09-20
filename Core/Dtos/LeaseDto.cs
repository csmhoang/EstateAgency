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
        public Guid? Id { get; }
        [Required(ErrorMessage = LeaseConst.ErrorEmptyTenantId)]
        public string? TenantId { get; set; }
        [Required(ErrorMessage = LeaseConst.ErrorEmptyRoomId)]
        public string? RoomId { get; set; }
        public int LeaseCode { get; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? SignedOnline { get; set; }
        public DateTime? SignedDate { get; set; }
        public string? Status { get; set; }
    }
}
