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
        (string.IsNullOrEmpty(specParams.Search) || x.Title.ToLower().Contains(specParams.Search))
        )
        {
            AddIncludes(new Expression<Func<Post, object>>[] {
                x => x.Room!
            });

            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        #endregion

    }
}
