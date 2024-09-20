using Core.Entities;
using Core.Interfaces.Data;
using Infrastructure.Data;
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
        public IQueryable<T> FindAll() => _context.Set<T>();
        public IQueryable<T> FindCondition(Expression<Func<T, bool>> expression) =>
            _context.Set<T>().Where(expression);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        #endregion
    }
}
