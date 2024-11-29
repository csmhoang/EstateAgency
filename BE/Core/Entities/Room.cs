using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.RoomEnums;

namespace Core.Entities
{
    public partial class Room
    {
        public Room()
        {
            Leases = new HashSet<Lease>();
            Amenities = new HashSet<Amenity>();
            Photos = new HashSet<Photo>();
            Posts = new HashSet<Post>();
            Reservations = new HashSet<Reservation>();
            BookingDetails = new HashSet<BookingDetail>();
            LeaseDetails = new HashSet<LeaseDetail>();
        }

        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Landlord")]
        [MaxLength(36)]
        public string? LandlordId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        [MaxLength(256)]
        public string Address { get; set; } = null!;
        [MaxLength(100)]
        public string Province { get; set; } = null!;
        [MaxLength(100)]
        public string District { get; set; } = null!;
        [MaxLength(100)]
        public string Ward { get; set; } = null!;
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int Toilet { get; set; }
        public Interior Interior { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Area { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public ConditionRoom Condition { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual User? Landlord { get; set; }
        public virtual ICollection<Lease> Leases { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Amenity> Amenities { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        public virtual ICollection<LeaseDetail> LeaseDetails { get; set; }
    }
}
