﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core;

public partial class Cart : BaseEntity
{
    public Cart()
    {
        CartDetails = new HashSet<CartDetail>();
    }
    [ForeignKey("Tenant")]
    [MaxLength(36)]
    public string? TenantId { get; set; }
    public virtual User? Tenant { get; set; }
    public virtual ICollection<CartDetail> CartDetails { get; set; }
}
