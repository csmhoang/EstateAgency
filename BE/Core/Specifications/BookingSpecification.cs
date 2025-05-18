using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class BookingSpecification : BaseSpecification<Booking>
{
    #region Constructor
    public BookingSpecification(BookingSpecParams specParams) : base(x =>
        (
            specParams.TenantId.Count == 0 ||
            specParams.TenantId.Contains(x.TenantId!)
        )
    &&
        (
            specParams.RoomId.Count == 0 ||
            x.BookingDetails.Any(bd => specParams.RoomId.Contains(bd.RoomId!))
        )
    &&
        (
            string.IsNullOrEmpty(specParams.Search) ||
            x.Tenant!.FullName.ToLower().Contains(specParams.Search) ||
            x.BookingDetails.Any(bd => bd.Room!.Landlord!.FullName.Contains(specParams.Search))
        )
    )
    {
        AddInclude(x => x
            .Include(b => b.BookingDetails!)
            .ThenInclude(bd => bd.Room!)
            .ThenInclude(r => r.Landlord!)
            .Include(b => b.Lease!)
            .ThenInclude(b => b.LeaseDetails!)
            .Include(b => b.Tenant!)
            .Include(b => b.Invoice!)
            .ThenInclude(i => i.InvoiceDetails)
            .Include(b => b.Invoice!)
            .ThenInclude(i => i.Payment!)
        );

        AddOrder(x => x.OrderByDescending(b => b.CreatedAt));

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
    #endregion
}
