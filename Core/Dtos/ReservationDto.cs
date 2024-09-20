using Core.Consts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record ReservationDto
    {
        public Guid? Id { get; }
        public int ReservationCode { get; }
        [Required(ErrorMessage = ReservationConst.ErrorEmptyTenantId)]
        public string? TenantId { get; set; }
        [Required(ErrorMessage = ReservationConst.ErrorEmptyRoomId)]
        public string? RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string? Status { get; set; }
    }
}
