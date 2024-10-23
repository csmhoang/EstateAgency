using Core.Consts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.ReservationEnums;

namespace Core.Dtos
{
    public record ReservationDto
    {
        public Guid? Id { get; set; }
        public int ReservationCode { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
        public string? TenantId { get; set; }
        [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
        public string? RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public StatusReservation? Status { get; set; }
    }
}
