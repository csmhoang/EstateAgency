using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record RoomDto
    {
        public int RoomCode { get; set; }
        public string Address { get; set; } = null!;
        public string? City { get; set; }
        public string? District { get; set; }
        public decimal Price { get; set; }
        public decimal? Area { get; set; }
        public string? Description { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public string? Status { get; set; }
    }
}
