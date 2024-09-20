using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record PaymentDto
    {
        public Guid? Id { get; }
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyLeaseId)]
        public string? LeaseId { get; set; }
        public int PaymentCode { get; }
        public decimal Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
