using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Core.Enums.LeaseEnums;

namespace Core.Entities;

public partial class Lease : BaseEntity
{
    public Lease()
    {
        LeaseDetails = new HashSet<LeaseDetail>();
    }
    [ForeignKey("Booking")]
    [MaxLength(36)]
    public string? BookingId { get; set; }
    [MaxLength(100)]
    public string Lessor { get; set; } = null!;
    [MaxLength(100)]
    public string Lessee { get; set; } = null!;
    public string? Terms { get; set; }
    [Column(TypeName = "date")]
    public DateTime? SignedDate { get; set; }
    public StatusLease Status { get; set; }

    public virtual Booking? Booking { get; set; }
    public virtual ICollection<LeaseDetail> LeaseDetails { get; set; }
}
