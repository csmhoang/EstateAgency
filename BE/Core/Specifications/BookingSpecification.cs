using Core.Entities;
using Core.Params;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BookingSpecification : BaseSpecification<Booking>
    {
        #region Constructor
        public BookingSpecification(BookingSpecParams specParams) : base(x =>
            (
                string.IsNullOrEmpty(specParams.Search) ||
                x.Room!.Name.ToLower().Contains(specParams.Search)
            )
        &&
            (
                specParams.TenantId.Count == 0 ||
                specParams.TenantId.Contains(x.TenantId!)
            )
        &&
            (
                specParams.RoomId.Count == 0 ||
                specParams.RoomId.Contains(x.RoomId!)
            )
        )
        {
            AddInclude(x => x
                .Include(r => r.Room!)
            );

            AddInclude(x => x
                .Include(r => r.Tenant!)
            );

            AddOrder(x => x.OrderBy(b => b.CreatedAt));

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        #endregion
    }
}
