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
                x.Post!.Room!.Name.ToLower().Contains(specParams.Search)
            )
        &&
            (
                specParams.TenantId.Count == 0 ||
                specParams.TenantId.Contains(x.TenantId!)
            )
        )
        {
            AddInclude(x => x
                .Include(r => r.Post!)
                .ThenInclude(p => p.Room!)
            );

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        #endregion
    }
}
