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
        [ForeignKey("Post")]
        public string? PostId { get; set; }
        public string? Note { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime ReservationDate { get; set; }
        public StatusReservation Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? Tenant { get; set; }
    }
}
