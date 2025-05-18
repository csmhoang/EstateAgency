using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Business;

public interface IRoomService
{
    /// <summary>
    /// Lấy ra tất cả phòng
    /// </summary>
    /// <returns>
    /// 1 - Danh sách phòng
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetAllAsync(string userId);
    /// <summary>
    /// Lấy danh sách thông tin phòng bằng specification
    /// </summary>
    /// <param name="specParams">Đối tượng tham số</param>
    /// <returns>
    /// 1 - Danh sách phòng
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetListAsync(RoomSpecParams specParams);
    /// <summary>
    /// Lấy ra phòng bằng id
    /// </summary>
    /// <param name="id">Id phòng</param>
    /// <returns>
    /// 1 - Phòng
    /// 2 - Null
    /// </returns>
    Task<Response> GetAsync(string id);
    /// <summary>
    /// Ẩn phòng bằng id
    /// </summary>
    /// <param name="id">Id phòng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> HideAsync(string id);
    /// <summary>
    /// Cập nhật phòng
    /// </summary>
    /// <param name="id">Id phòng</param>
    /// <param name="roomUpdateDto">Phòng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> UpdateAsync(string id, RoomUpdateDto roomUpdateDto);
    /// <summary>
    /// Thêm phòng
    /// </summary>
    /// <param name="roomDto">Phòng</param>
    /// <param name="files">Danh sách tệp phòng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> InsertAsync(RoomDto roomDto, IFormFile[]? files);
    /// <summary>
    /// Thêm ảnh cho phòng
    /// </summary>
    /// <param name="roomId">Mã phòng</param>
    /// <param name="file">Tệp ảnh</param>
    /// <returns>
    /// Ảnh phòng thêm
    /// </returns>
    Task<Response> InsertPhotoAsync(string roomId, IFormFile file);
    /// <summary>
    /// Xóa ảnh phòng
    /// </summary>
    /// <param name="roomId">Mã phòng</param>
    /// <param name="photoId">Mã ảnh</param>
    /// <returns>        
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> DeletePhotoAsync(string roomId, string photoId);

}
