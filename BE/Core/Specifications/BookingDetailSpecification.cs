using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core;

public class BookingDetailSpecification : BaseSpecification<BookingDetail>
{
    #region Constructor
    public BookingDetailSpecification(BookingDetailSpecParams specParams, Expression<Func<BookingDetail, bool>>? lambda) : base(x =>
        (
            specParams.TenantId.Count == 0 ||
            specParams.TenantId.Contains(x.Booking!.TenantId!)
        )
    &&
        (
            string.IsNullOrEmpty(specParams.Search) ||
            x.Room!.Name.ToLower().Contains(specParams.Search)
        )
    )
    {
        AddInclude(x => x
            .Include(bd => bd.Room!)
            .ThenInclude(r => r.Landlord!)
            .Include(bd => bd.Room!)
            .ThenInclude(r => r.Photos!)
            .Include(x => x.Booking!)
            .ThenInclude(b => b.Lease!)
            .ThenInclude(l => l.LeaseDetails!)
        );

        if (lambda != null) AddLambda(x => x.Where(lambda));

        AddOrder(x => x.OrderByDescending(b => b.CreatedAt));

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
    #endregion
}
