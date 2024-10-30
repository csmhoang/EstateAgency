using Core.Interfaces.Specifications;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : class
    {
        #region Declaration
        private readonly Expression<Func<T, bool>>? criteria;
        public Expression<Func<T, bool>>? Criteria => criteria;
        #endregion

        #region Property
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        public Expression<Func<T, object>>[]? Includes { get; private set; }
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
        protected BaseSpecification() : this(null) { }
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

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
        protected void AddIncludes(Expression<Func<T, object>>[] includesExpression)
        {
            Includes = includesExpression;
        }

        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }

        protected void ApplyPaging(int skip, int take)
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
        protected BaseSpecification() : this(null) { }
        #endregion

        #region Method
        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
        #endregion
    }


}
