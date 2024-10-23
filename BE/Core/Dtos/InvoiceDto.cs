using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.InvoiceEnums;

namespace Core.Dtos
{
    public record InvoiceDto
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = LeaseConst.ErrorEmptyId)]
        public string? LeaseId { get; set; }
        public int InvoiceCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusInvoice? Status { get; set; }
    }
}
