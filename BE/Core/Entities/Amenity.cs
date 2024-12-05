using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Amenity : BaseEntity
    {
        public Amenity()
        {
            Rooms = new HashSet<Room>();
        }

        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
