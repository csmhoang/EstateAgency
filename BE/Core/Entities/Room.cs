﻿using System;
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
            Photos = new HashSet<Photo>();
        }

        public string Id { get; set; } = null!;
        public string? LandlordId { get; set; }
        public int RoomCode { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public decimal Price { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
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
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Amenity> Amenities { get; set; }
    }
}
