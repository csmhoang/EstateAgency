using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.ReservationEnums;

namespace Core.Dtos
{
    public record ReservationUpdateDto
    {
        public string? Note { get; set; }
        public DateTime ReservationDate { get; set; }
        public int ReservationHour { get; set; }
        public int ReservationMinute { get; set; }
    }
}
