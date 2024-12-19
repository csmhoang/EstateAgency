using Core.Entities;
using Core.Params;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class PostSpecification : BaseSpecification<Post>
    {
        #region Constructor
        public PostSpecification(PostSpecParams specParams) : base(x =>
            (
                specParams.LandlordId.Count == 0 ||
                specParams.LandlordId.Contains(x.LandlordId!)
            )
        &&
            (
                string.IsNullOrEmpty(specParams.Search) ||
                x.Title.ToLower().Contains(specParams.Search) ||
                x.Room!.Name.ToLower().Contains(specParams.Search) ||
                x.Room!.Address.ToLower().Contains(specParams.Search)
            )
        &&
            (
                specParams.Province.Count == 0 ||
                specParams.Province.Contains(x.Room!.Province!)
            )
        &&
            (
                specParams.Category == null ||
                specParams.Category == x.Room!.Category
            )
        &&
            (
                specParams.Status == null ||
                specParams.Status == x.Status
            )
        &&
            (
                specParams.IsAccept == null ||
                specParams.IsAccept == x.IsAccept
            )
        &&
            (
                specParams.MinPrice == null ||
                specParams.MaxPrice == null ||
                (x.Room!.Price >= specParams.MinPrice && x.Room!.Price <= specParams.MaxPrice)
            )
        &&
            (
                specParams.MinArea == null ||
                specParams.MaxArea == null ||
                (x.Room!.Area >= specParams.MinArea && x.Room!.Area <= specParams.MaxArea)
            )
        )
        {
            AddInclude(x => x
                .Include(p => p.Room!)
                .ThenInclude(r => r.Photos)
            );

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);


            switch (specParams.SortPrice)
            {
                case "PriceAsc": AddOrder(x => x.OrderBy(p => p.Room!.Price)); break;
                case "PriceDesc": AddOrder(x => x.OrderByDescending(p => p.Room!.Price)); break;
            }

            switch (specParams.SortArea)
            {
                case "AreaAsc": AddOrder(x => x.OrderBy(p => p.Room!.Area)); break;
                case "AreaDesc": AddOrder(x => x.OrderByDescending(p => p.Room!.Price)); break;
            }

            switch (specParams.SortExtra)
            {
                case "New": AddOrder(x => x.OrderByDescending(p => p.CreatedAt)); break;
                case "Favorite": AddOrder(x => x.OrderBy(p => p.SavePosts.Count)); break;
                case "New/Favorite":
                    AddOrder(x => x
                        .OrderByDescending(p => p.CreatedAt)
                        .ThenBy(p => p.SavePosts.Count)
                    );
                    break;
                default:
                    AddOrder(x => x.OrderByDescending(b => b.CreatedAt));
                    break;
            }
        }
        #endregion
    }
}
