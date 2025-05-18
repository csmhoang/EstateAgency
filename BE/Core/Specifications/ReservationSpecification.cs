using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class ReservationSpecification : BaseSpecification<Reservation>
{
    #region Constructor
    public ReservationSpecification(ReservationSpecParams specParams) : base(x =>
        (
            specParams.TenantId.Count == 0 ||
            specParams.TenantId.Contains(x.TenantId!)
        )
    &&
        (
            specParams.RoomId.Count == 0 ||
            specParams.RoomId.Contains(x.RoomId!)
        )
    &&
        (
            string.IsNullOrEmpty(specParams.Search) ||
            x.Room!.Name.ToLower().Contains(specParams.Search)
        )
    )
    {
        AddInclude(x => x
            .Include(r => r.Room!)
            .Include(r => r.Tenant!)
        );

        AddOrder(x => x.OrderByDescending(b => b.CreatedAt));

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
    #endregion
}
