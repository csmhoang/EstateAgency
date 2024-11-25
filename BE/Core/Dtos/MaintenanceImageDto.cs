using Core.Consts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record MaintenanceImageDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = MaintenanceRequestConst.ErrorEmptyId)]
        public string MaintenanceRequestId { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string PublicId { get; set; } = null!;
    }
}
