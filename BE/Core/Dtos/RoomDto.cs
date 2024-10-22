using Core.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record RoomDto
    {
        public Guid? Id { get; set; }
        public int RoomCode { get; set; }
        [Required(ErrorMessage = RoomConst.ErrorEmptyLandlordId)]
        public string? LandlordId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Ward { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public decimal Price { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public decimal Area { get; set; }
        public string? Description { get; set; }
        public DateTime AvailableFrom { get; set; }
        public string? Status { get; set; }

    }
}
