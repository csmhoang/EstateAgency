namespace Core.Interfaces.Business;

public interface IAmenityService
{
    /// <summary>
    /// Lấy ra tất cả tiện nghi
    /// </summary>
    /// <returns>
    /// 1 - Danh sách tiện nghi
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetAllAsync();
    /// <summary>
    /// Lấy ra tiện nghi bằng id
    /// </summary>
    /// <param name="id">Id tiện nghi</param>
    /// <returns>
    /// 1 - Tiện nghi
    /// 2 - Null
    /// </returns>
    Task<Response> GetAsync(string id);
    /// <summary>
    /// Xóa tiện nghi bằng id
    /// </summary>
    /// <param name="id">Id tiện nghi</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> DeleteAsync(string id);
    /// <summary>
    /// Cập nhật tiện nghi
    /// </summary>
    /// <param name="id">Id tiện nghi</param>
    /// <param name="amenityDto">Tiện nghi</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> UpdateAsync(string id, AmenityDto amenityDto);
    /// <summary>
    /// Thêm tiện nghi
    /// </summary>
    /// <param name="amenityDto">Tiện nghi</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> InsertAsync(AmenityDto amenityDto);
}
