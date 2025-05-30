﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Booking : BaseEntity
{
    public Booking()
    {
        BookingDetails = new HashSet<BookingDetail>();
    }
    [ForeignKey("Tenant")]
    [MaxLength(36)]
    public string? TenantId { get; set; }
    [ForeignKey("Invoice")]
    [MaxLength(36)]
    public string? InvoiceId { get; set; }
    public BookingEnums.StatusBooking Status { get; set; }

    public virtual User? Tenant { get; set; }
    public virtual Invoice? Invoice { get; set; }
    public virtual Lease? Lease { get; set; }
    public virtual ICollection<BookingDetail> BookingDetails { get; set; }
}
