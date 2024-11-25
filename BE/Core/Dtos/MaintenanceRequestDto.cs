using Core.Consts;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.MaintenanceRequestEnums;

namespace Core.Dtos
{
    public record MaintenanceRequestDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
        public string TenantId { get; set; } = null!;
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyId)]
        public string InvoiceId { get; set; } = null!;
        public string? Description { get; set; }
        public string? RejectionReason { get; set; }
        public decimal? EstimateCost { get; set; }
        public StatusMaintenanceRequest? Status { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<MaintenanceImageDto>? MaintenanceImages { get; set; }
    }
}
