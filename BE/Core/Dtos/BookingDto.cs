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
        [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
        public string RoomId { get; set; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
        public string TenantId { get; set; } = null!;
        public string? InvoiceId { get; set; }
        public DateTime IntendedIntoDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? RejectionReason { get; set; }
        public string? Note { get; set; }
        public StatusBooking? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDto? Tenant { get; set; }
        public RoomDto? Room { get; set; }
    }
}
