using Core.Entities;
using Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class RoomSpecification : BaseSpecification<Room>
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public RoomSpecification(RoomSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (specParams.Province.Count == 0 || specParams.Province.Contains(x.Province!))
        )
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "priceAsc": AddOrderBy(x => x.Price); break;
                case "priceDesc": AddOrderByDescending(x => x.Price); break;
                default: AddOrderBy(x => x.Name); break;
            }
        }
        #endregion

        #region Method
        #endregion

    }
}
