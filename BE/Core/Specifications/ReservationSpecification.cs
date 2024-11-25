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
    public class ReservationSpecification : BaseSpecification<Reservation>
    {
        #region Constructor
        public ReservationSpecification(ReservationSpecParams specParams) : base(x =>
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

            AddOrder(x => x.OrderBy(r => r.CreatedAt));

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        #endregion
    }
}
