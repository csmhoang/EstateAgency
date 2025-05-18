using static Core.Enums.BookingEnums;

namespace Core.Interfaces.Business;

public interface IBookingService
{
    /// <summary>
    /// Lấy ra tất cả đặt phòng
    /// </summary>
    /// <returns>
    /// 1 - Danh sách đặt phòng
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetAllAsync();
    /// <summary>
    /// Lấy ra đặt phòng bằng id
    /// </summary>
    /// <param name="id">Id đặt phòng</param>
    /// <returns>
    /// 1 - Đặt phòng
    /// 2 - Null
    /// </returns>
    Task<Response> GetAsync(string id);
    /// <summary>
    /// Lấy danh sách thông tin đặt phòng bằng specification
    /// </summary>
    /// <param name="specParams">Đối tượng tham số</param>
    /// <returns>
    /// 1 - Danh sách đặt phòng
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetListAsync(BookingSpecParams specParams);
    /// <summary>
    /// Phản hồi đặt phòng
    /// </summary>
    /// <param name="id">Id đặt phòng</param>
    /// <param name="status">Trạng thái</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> ResponseAsync(string id, StatusBooking status);
    /// <summary>
    /// Phản hồi chi tiết đặt phòng 
    /// </summary>
    /// <param name="bookingDetailId">Id chi tiết đặt phòng</param>
    /// <param name="status">Trạng thái</param>
    /// <param name="RejectionReason">Lý do</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> ResponseDetailAsync(string bookingDetailId, StatusBookingDetail status, string? RejectionReason);
    /// <summary>
    /// Thêm đặt phòng từ giỏ phòng
    /// </summary>
    /// <param name="userId">Id người dùng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> InsertAsync(string userId);
}
