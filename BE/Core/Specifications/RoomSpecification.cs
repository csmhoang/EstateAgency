using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class RoomSpecification : BaseSpecification<Room>
{
    #region Constructor
    public RoomSpecification(RoomSpecParams specParams) : base(x =>
        (
            specParams.LandlordId.Count == 0 ||
            specParams.LandlordId.Contains(x.LandlordId!)
        )
    &&
        (
            string.IsNullOrEmpty(specParams.Search) ||
            x.Name.ToLower().Contains(specParams.Search)
        )
    && (x.Visibility != null && x.Visibility == true)
    )
    {
        AddInclude(x => x.Include(r => r.Photos));

        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        AddOrder(x => x.OrderByDescending(b => b.CreatedAt));

        switch (specParams.Sort)
        {
            case "priceAsc": AddOrder(x => x.OrderBy(r => r.Price)); break;
            case "priceDesc": AddOrder(x => x.OrderByDescending(r => r.Price)); break;
            default: AddOrder(x => x.OrderBy(r => r.Name)); break;
        }
    }
    #endregion
}
