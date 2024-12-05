using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.ReservationEnums;

namespace Core.Entities
{
    public partial class Reservation : BaseEntity
    {
        [ForeignKey("Tenant")]
        [MaxLength(36)]
        public string? TenantId { get; set; }
        [ForeignKey("Room")]
        [MaxLength(36)]
        public string? RoomId { get; set; }
        public string? Note { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime ReservationDate { get; set; }
        public StatusReservation Status { get; set; }

        public virtual Room? Room { get; set; }
        public virtual User? Tenant { get; set; }
    }
}
