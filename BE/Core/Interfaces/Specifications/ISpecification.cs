using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Interfaces.Specifications;

public interface ISpecification<T> where T : class
{
    Expression<Func<T, bool>>? Criteria { get; }
    Func<IQueryable<T>, IQueryable<T>>? Lambda { get; }
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? Include { get; }
    List<Func<IQueryable<T>, IOrderedQueryable<T>>> Orders { get; }
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
