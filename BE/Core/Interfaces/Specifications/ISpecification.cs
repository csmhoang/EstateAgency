﻿using System.Linq.Expressions;

namespace Core.Interfaces.Specifications
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>>? Criteria { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        bool IsDistinct { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        IQueryable<T> ApplyCriteria(IQueryable<T> query);
    }

    public interface ISpecification<T, TResult> : ISpecification<T> where T : class
    {
        Expression<Func<T, TResult>>? Select { get; }
    }

}
