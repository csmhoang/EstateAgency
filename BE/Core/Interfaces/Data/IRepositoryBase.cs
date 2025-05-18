using System.Linq.Expressions;

namespace Core;

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
    /// <summary>
    /// Lấy ra bản ghi đầu tiên bằng spec
    /// </summary>
    /// <param name="spec">Biểu thức Lambda</param>
    /// <returns>
    /// 1 - Bản ghi
    /// 2 - Null
    /// </returns>
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    /// <summary>
    /// Lấy ra danh sách bản ghi bằng spec
    /// </summary>
    /// <param name="spec">Biểu thức Lambda</param>
    /// <returns>
    /// 1 - Danh sách bản ghi
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    /// <summary>
    /// Lấy ra bản ghi đầu tiên bằng spec
    /// </summary>
    /// <typeparam name="TResult">Kiểu trả về</typeparam>
    /// <param name="spec">Biểu thức Lambda</param>
    /// <returns>
    /// 1 - Bản ghi
    /// 2 - Null
    /// </returns>
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    /// <summary>
    /// Lấy ra danh sách bản ghi bằng spec
    /// </summary>
    /// <typeparam name="TResult">Kiểu trả về</typeparam>
    /// <param name="spec">Biểu thức Lambda</param>
    /// <returns>
    /// 1 - Danh sách bản ghi
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);
    /// <summary>
    /// Số bản ghi của bảng
    /// </summary>
    /// <param name="spec">Biểu thức Lambda</param>
    /// <returns>Số bản ghi</returns>
    Task<int> CountAsync(ISpecification<T> spec);
}
