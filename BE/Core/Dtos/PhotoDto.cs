using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record PhotoDto
    {
        public Guid? Id { get; set; }
        public string? RoomId { get; set; }
        public string Url { get; set; } = null!;
        public string PublicId { get; set; } = null!;
    }
}
