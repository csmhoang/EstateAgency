using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Room
    {
        public Room()
        {
            Feedbacks = new HashSet<Feedback>();
            Leases = new HashSet<Lease>();
            Reservations = new HashSet<Reservation>();
            Amenities = new HashSet<Amenity>();
        }

        public string Id { get; set; } = null!;
        public string? LandlordId { get; set; }
        public int RoomCode { get; set; }
        public string Address { get; set; } = null!;
        public string? City { get; set; }
        public string? District { get; set; }
        public decimal Price { get; set; }
        public decimal? Area { get; set; }
        public string? Description { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? Landlord { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<Amenity> Amenities { get; set; }
    }
}
