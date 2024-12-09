using Core.Interfaces.Specifications;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public abstract class SpecificationEvaluator<T> where T : class
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        #endregion

        #region Method
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if (spec.Include != null)
            {
                query = spec.Include(query);
            }

            if (spec.Lambda != null)
            {
                query = spec.Lambda(query);
            }

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.IsDistinct)
            {
                query = query.Distinct();
            }

            if (spec.Orders.Any())
            {
                foreach (var order in spec.Orders)
                {
                    query = order(query);
                }
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query,
            ISpecification<T, TResult> spec)
        {
            if (spec.Include != null)
            {
                query = spec.Include(query);
            }

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.Orders.Any())
            {
                foreach (var order in spec.Orders)
                {
                    query = order(query);
                }
            }

            var selectQuery = query as IQueryable<TResult>;

            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }

            if (spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }

            if (spec.IsPagingEnabled)
            {
                selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
            }

            return selectQuery ?? query.Cast<TResult>();
        }
        #endregion

    }
}
