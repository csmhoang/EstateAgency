using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public partial class Amenity
    {
        public Amenity()
        {
            Rooms = new HashSet<Room>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
