using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record InvoiceDetailDto
    {
        public string? Id { get; set; }
        public string? InvoiceId { get; set; }
        public string Detail { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
