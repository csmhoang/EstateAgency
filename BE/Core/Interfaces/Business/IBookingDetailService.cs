using Core.Dtos;
using Core.Params;

namespace Core.Interfaces.Business
{
    public interface IBookingDetailService
    {
        /// <summary>
        /// Lấy ra tất cả chi tiết đặt phòng
        /// </summary>
        /// <returns>
        /// 1 - Danh sách chi tiết đặt phòng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
        /// <summary>
        /// Lấy ra chi tiết đặt phòng bằng id
        /// </summary>
        /// <param name="id">Id chi tiết đặt phòng</param>
        /// <returns>
        /// 1 - Đặt phòng
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string id);
        /// <summary>
        /// Lấy danh sách chi tiết đặt phòng đang thuê
        /// </summary>
        /// <param name="specParams">Đối tương tham số</param>
        /// <returns>
        /// 1 - Danh sách chi tiết đặt phòng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetRentedBookingDetailsAsync(BookingDetailSpecParams specParams);
    }
}
