using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Consts;

namespace Core.Dtos
{
    public record LeaseDetailDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = BookingConst.ErrorEmptyId)]
        public string BookingId { get; set; } = null!;
        [Required(ErrorMessage = LeaseConst.ErrorEmptyId)]
        public string LeaseId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual BookingDto? Booking { get; set; }
        public virtual LeaseDto? Lease { get; set; }
    }
}
