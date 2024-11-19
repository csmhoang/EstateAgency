using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
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
        /// Xóa đặt phòng bằng id
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> DeleteAsync(string id);
        /// <summary>
        /// Cập nhật đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <param name="bookingDto">Đặt phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, BookingDto bookingDto);
        /// <summary>
        /// Thêm đặt phòng
        /// </summary>
        /// <param name="bookingDto">Đặt phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(BookingDto bookingDto);
    }
}
