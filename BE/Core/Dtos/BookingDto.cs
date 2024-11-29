using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.BookingEnums;

namespace Core.Dtos
{
    public record BookingDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
        public string TenantId { get; set; } = null!;
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyId)]
        public string? InvoiceId { get; set; }
        public string? RejectionReason { get; set; }
        public string? Note { get; set; }
        public StatusBooking? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UserDto? Tenant { get; set; }
        public InvoiceDto? Invoice { get; set; }
        public ICollection<BookingDetailDto>? BookingDetails { get; set; }
    }
}
