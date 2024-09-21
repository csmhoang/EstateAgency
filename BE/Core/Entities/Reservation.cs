using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Reservation
    {
        public string Id { get; set; } = null!;
        public string? TenantId { get; set; }
        public string? RoomId { get; set; }
        public int ReservationCode { get; set; }
        public DateTime ReservationDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Room? Room { get; set; }
        public virtual User? Tenant { get; set; }
    }
}
