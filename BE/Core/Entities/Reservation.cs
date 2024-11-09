using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.ReservationEnums;

namespace Core.Entities
{
    public partial class Reservation
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Tenant")]
        public string? TenantId { get; set; }
        [ForeignKey("Room")]
        public string? RoomId { get; set; }
        public DateTime ReservationDate { get; set; }
        public StatusReservation Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Room? Room { get; set; }
        public virtual User? Tenant { get; set; }
    }
}
