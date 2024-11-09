using Core.Entities;
using Core.Params;
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
            AddIncludes(new Expression<Func<Post, object>>[] {
                x => x.Room!,
                x=>x.Room!.Photos
            });

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);


            switch (specParams.SortPrice)
            {
                case "PriceAsc": AddOrderBy(x => x.Room!.Price); break;
                case "PriceDesc": AddOrderByDescending(x => x.Room!.Price); break;
            }

            switch (specParams.SortArea)
            {
                case "AreaAsc": AddOrderBy(x => x.Room!.Area); break;
                case "AreaDesc": AddOrderByDescending(x => x.Room!.Area); break;
            }

            switch (specParams.SortExtra)
            {
                case "New": AddOrderBy(x => x.CreatedAt!); break;
                case "Favorite": AddOrderBy(x => x.SavePosts.Count()); break;
                case "New/Favorite":
                    AddOrderThenBy(new Expression<Func<Post, object>>[] {
                        x => x.CreatedAt!,
                        x => x.SavePosts.Count()
                    });
                    break;
                default: AddOrderBy(x => x.Title); break;
            }
        }
        #endregion
    }
}
