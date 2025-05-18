using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public partial class LeaseDetail : BaseEntity
{
    [ForeignKey("Room")]
    [MaxLength(36)]
    public string RoomId { get; set; } = null!;
    [ForeignKey("Lease")]
    [MaxLength(36)]
    public string? LeaseId { get; set; }
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public int NumberOfTenant { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public virtual Room? Room { get; set; }
    public virtual Lease? Lease { get; set; }
}
