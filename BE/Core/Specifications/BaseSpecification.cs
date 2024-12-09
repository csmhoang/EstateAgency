using Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        #region Declaration
        private readonly Expression<Func<T, bool>>? criteria;
        public Expression<Func<T, bool>>? Criteria => criteria;
        #endregion

        #region Property
        public Func<IQueryable<T>, IQueryable<T>>? Lambda { get; private set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>>? Include { get; private set;}
        public List<Func<IQueryable<T>, IOrderedQueryable<T>>> Orders { get; }
            = new List<Func<IQueryable<T>, IOrderedQueryable<T>>>();
        public bool IsDistinct { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }
        #endregion

        #region Constructor
        public BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            this.criteria = criteria;
        }
        public BaseSpecification() : this(null) { }
        #endregion

        #region Method
        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if (Criteria != null)
            {
                query = query.Where(Criteria);
            }
            return query;
        }
        public void AddLambda(Func<IQueryable<T>, IQueryable<T>> lambda)
        {
            Lambda = lambda;
        }
        public void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            Include = includeExpression;
        }
        public void AddOrder(Func<IQueryable<T>, IOrderedQueryable<T>> orderExpression)
        {
            Orders.Add(orderExpression);
        }


        public void ApplyDistinct()
        {
            IsDistinct = true;
        }

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        #endregion
    }

    public abstract class BaseSpecification<T, TResult>
        : BaseSpecification<T>, ISpecification<T, TResult> where T : class
    {
        #region Declaration
        #endregion

        #region Property
        public Expression<Func<T, TResult>>? Select { get; private set; }
        #endregion

        #region Constructor
        public BaseSpecification(Expression<Func<T, bool>>? criteria) : base(criteria) { }
        public BaseSpecification() : this(null) { }
        #endregion

        #region Method
        public void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
        #endregion
    }


}
