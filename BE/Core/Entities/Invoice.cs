﻿using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public partial class Invoice
    {
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = InvoiceConst.ErrorEmptyLeaseId)]
        public string? LeaseId { get; set; }
        public int InvoiceCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Lease? Lease { get; set; }
    }
}
