using Core.Consts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record MaintenanceRequestDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = MaintenanceRequestConst.ErrorEmptyLeaseId)]
        public string? LeaseId { get; set; }
        public int RequestCode { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? RequestDate { get; set; }
        public string? Status { get; set; }
    }
}
