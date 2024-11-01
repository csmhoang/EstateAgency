﻿using Core.Interfaces.Specifications;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (spec.Includes != null)
            {
                foreach (var inculde in spec.Includes)
                {
                    query = query.Include(inculde);
                }
            }

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsDistinct)
            {
                query = query.Distinct();
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
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
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
