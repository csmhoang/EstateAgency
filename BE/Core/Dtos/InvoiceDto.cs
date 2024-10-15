using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record InvoiceDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyLeaseId)]
        public string? LeaseId { get; set; }
        public int InvoiceCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
    }
}
