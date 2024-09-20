using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Data
{
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>
        /// 1 - Danh sách bản ghi
        /// 2 - Danh sách rỗng
        /// </returns>
        IQueryable<T> FindAll();
        /// <summary>
        /// Lấy ra tất cả bản ghi theo Lambda
        /// </summary>
        /// <param name="expression">Điều kiện</param>
        /// <returns>
        /// 1 - Danh sách bản ghi
        /// 2 - Danh sách rỗng
        /// </returns>
        IQueryable<T> FindCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>
        /// Số bản ghi thêm thành công
        /// </returns>
        void Create(T entity);
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        void Update(T entity);
        /// <summary>
        /// Xóa một bản ghi bằng id
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        void Delete(T entity);
    }
}
