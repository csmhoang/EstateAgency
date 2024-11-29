using Core.Consts;
using Core.Entities;
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
        public string? Id { get; set; }
        [Required(ErrorMessage = PaymentConst.ErrorEmptyId)]
        public string InvoiceId { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
