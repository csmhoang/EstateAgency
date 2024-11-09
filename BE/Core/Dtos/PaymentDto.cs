using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.PaymentEnums;

namespace Core.Dtos
{
    public record PaymentDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = LeaseConst.ErrorEmptyId)]
        public string? LeaseId { get; set; }
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyId)]
        public string? InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public StatusPayment? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
