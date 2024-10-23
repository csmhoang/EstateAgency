using Core.Consts;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.MaintenanceRequestEnums;

namespace Core.Dtos
{
    public record MaintenanceRequestDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = LeaseConst.ErrorEmptyId)]
        public string? LeaseId { get; set; }
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyId)]
        public string? InvoiceId { get; set; }
        public int MaintenanceRequestCode { get; set; }
        public string Description { get; set; } = null!;
        public DateTime? RequestDate { get; set; }
        public StatusMaintenanceRequest? Status { get; set; }
    }
}
