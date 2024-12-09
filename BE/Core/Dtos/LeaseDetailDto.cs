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
        [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
        public string RoomId { get; set; } = null!;
        [Required(ErrorMessage = LeaseConst.ErrorEmptyId)]
        public string LeaseId { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfTenant { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual RoomDto? Room { get; set; }
        public virtual LeaseDto? Lease { get; set; }
    }
}
