using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

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
            case "PriceAsc":
                {
                    switch (specParams.SortArea)
                    {
                        case "AreaAsc":
                            {
                                switch (specParams.SortExtra)
                                {
                                    case "New": AddOrder(x => x.OrderBy(p => p.Room!.Price).ThenBy(p => p.Room!.Area).ThenByDescending(p => p.CreatedAt)); return;
                                    case "Favorite": AddOrder(x => x.OrderBy(p => p.Room!.Price).ThenBy(p => p.Room!.Area).ThenByDescending(p => p.SavePosts.Count)); return;
                                    case "New/Favorite":
                                        AddOrder(x => x
                                            .OrderBy(p => p.Room!.Price)
                                            .ThenBy(p => p.Room!.Area)
                                            .ThenByDescending(p => p.CreatedAt)
                                            .ThenByDescending(p => p.SavePosts.Count)
                                        );
                                        return;
                                }
                                AddOrder(x => x.OrderBy(p => p.Room!.Price).ThenBy(p => p.Room!.Area)); return;
                            }
                        case "AreaDesc":
                            {
                                switch (specParams.SortExtra)
                                {
                                    case "New": AddOrder(x => x.OrderBy(p => p.Room!.Price).ThenByDescending(p => p.Room!.Area).ThenByDescending(p => p.CreatedAt)); return;
                                    case "Favorite": AddOrder(x => x.OrderBy(p => p.Room!.Price).ThenByDescending(p => p.Room!.Area).ThenByDescending(p => p.SavePosts.Count)); return;
                                    case "New/Favorite":
                                        AddOrder(x => x
                                            .OrderBy(p => p.Room!.Price)
                                            .ThenByDescending(p => p.Room!.Area)
                                            .ThenByDescending(p => p.CreatedAt)
                                            .ThenByDescending(p => p.SavePosts.Count)
                                        );
                                        return;
                                }
                                AddOrder(x => x.OrderBy(p => p.Room!.Price).ThenByDescending(p => p.Room!.Area)); return;
                            }
                    }
                    AddOrder(x => x.OrderBy(p => p.Room!.Price)); return;
                }
            case "PriceDesc":
                {
                    switch (specParams.SortArea)
                    {
                        case "AreaAsc":
                            {
                                switch (specParams.SortExtra)
                                {
                                    case "New": AddOrder(x => x.OrderByDescending(p => p.Room!.Price).ThenBy(p => p.Room!.Area).ThenByDescending(p => p.CreatedAt)); return;
                                    case "Favorite": AddOrder(x => x.OrderByDescending(p => p.Room!.Price).ThenBy(p => p.Room!.Area).ThenByDescending(p => p.SavePosts.Count)); return;
                                    case "New/Favorite":
                                        AddOrder(x => x
                                            .OrderByDescending(p => p.Room!.Price)
                                            .ThenBy(p => p.Room!.Area)
                                            .ThenByDescending(p => p.CreatedAt)
                                            .ThenByDescending(p => p.SavePosts.Count)
                                        );
                                        return;
                                }
                                AddOrder(x => x.OrderByDescending(p => p.Room!.Price).ThenBy(p => p.Room!.Area)); return;
                            }
                        case "AreaDesc":
                            {
                                switch (specParams.SortExtra)
                                {
                                    case "New": AddOrder(x => x.OrderByDescending(p => p.Room!.Price).ThenByDescending(p => p.Room!.Area).ThenByDescending(p => p.CreatedAt)); return;
                                    case "Favorite": AddOrder(x => x.OrderByDescending(p => p.Room!.Price).ThenByDescending(p => p.Room!.Area).ThenByDescending(p => p.SavePosts.Count)); return;
                                    case "New/Favorite":
                                        AddOrder(x => x
                                            .OrderByDescending(p => p.Room!.Price)
                                            .ThenByDescending(p => p.Room!.Area)
                                            .ThenByDescending(p => p.CreatedAt)
                                            .ThenByDescending(p => p.SavePosts.Count)
                                        );
                                        return;
                                }
                                AddOrder(x => x.OrderByDescending(p => p.Room!.Price).ThenByDescending(p => p.Room!.Area)); return;
                            }
                    }
                    AddOrder(x => x.OrderByDescending(p => p.Room!.Price)); return;
                }
        }

        switch (specParams.SortArea)
        {
            case "AreaAsc":
                {
                    switch (specParams.SortExtra)
                    {
                        case "New": AddOrder(x => x.OrderBy(p => p.Room!.Area).OrderByDescending(p => p.CreatedAt)); return;
                        case "Favorite": AddOrder(x => x.OrderBy(p => p.Room!.Area).OrderByDescending(p => p.SavePosts.Count)); return;
                        case "New/Favorite":
                            AddOrder(x => x
                                .OrderBy(p => p.Room!.Area)
                                .OrderByDescending(p => p.CreatedAt)
                                .ThenByDescending(p => p.SavePosts.Count)
                            );
                            return;
                    }
                    AddOrder(x => x.OrderBy(p => p.Room!.Area)); return;
                }
            case "AreaDesc":
                {
                    switch (specParams.SortExtra)
                    {
                        case "New": AddOrder(x => x.OrderByDescending(p => p.Room!.Area).OrderByDescending(p => p.CreatedAt)); return;
                        case "Favorite": AddOrder(x => x.OrderByDescending(p => p.Room!.Area).OrderByDescending(p => p.SavePosts.Count)); return;
                        case "New/Favorite":
                            AddOrder(x => x
                                .OrderByDescending(p => p.Room!.Area)
                                .OrderByDescending(p => p.CreatedAt)
                                .ThenByDescending(p => p.SavePosts.Count)
                            );
                            return;
                    }
                    AddOrder(x => x.OrderByDescending(p => p.Room!.Price)); return;
                }
        }

        switch (specParams.SortExtra)
        {
            case "New": AddOrder(x => x.OrderByDescending(p => p.CreatedAt)); return;
            case "Favorite": AddOrder(x => x.OrderByDescending(p => p.SavePosts.Count)); return;
            case "New/Favorite":
                AddOrder(x => x
                    .OrderByDescending(p => p.CreatedAt)
                    .ThenByDescending(p => p.SavePosts.Count)
                );
                return;
            default:
                AddOrder(x => x.OrderByDescending(b => b.CreatedAt));
                return;
        }
    }
    #endregion
}
