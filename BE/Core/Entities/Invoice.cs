using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.InvoiceEnums;

namespace Core.Entities
{
    public partial class Invoice: BaseEntity
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public StatusInvoice Status { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
