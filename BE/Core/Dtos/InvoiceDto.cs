﻿using Core.Consts;
using Core.Entities;
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
        public string? Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public StatusInvoice? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<InvoiceDetailDto>? InvoiceDetails { get; set; }
        public BookingDto? Booking { get; set; }
        public PaymentDto? Payment { get; set; }
    }
}
