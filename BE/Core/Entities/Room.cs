using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.RoomEnums;

namespace Core.Entities
{
    public partial class Room
    {
        public Room()
        {
            Leases = new HashSet<Lease>();
            Reservations = new HashSet<Reservation>();
            Amenities = new HashSet<Amenity>();
            Photos = new HashSet<Photo>();
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; } = null!;
        [ForeignKey("Landlord")]
        public string? LandlordId { get; set; }
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        public string Address { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int Toilet { get; set; }
        public Interior Interior { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public ConditionRoom Condition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? Landlord { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Amenity> Amenities { get; set; }
    }
}
