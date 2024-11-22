using Core.Entities;
using Core.Interfaces.Data;
using Core.Interfaces.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Declaration
        protected readonly RepositoryContext _context;
        #endregion

        #region Property
        #endregion

        #region Constructor

        protected RepositoryBase(RepositoryContext context) =>
            _context = context;
        #endregion

        #region Method
        public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();
        public IQueryable<T> FindCondition(Expression<Func<T, bool>> expression) =>
            _context.Set<T>().AsNoTracking().Where(expression);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().ToListAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T, TResult>(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();
            query = spec.ApplyCriteria(query);
            return await query.CountAsync();
        }
        #endregion
    }
}
