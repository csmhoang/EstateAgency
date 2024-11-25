using Core.Dtos;
using Core.Params;
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
        /// Lấy danh sách thông tin đặt phòng bằng specification
        /// </summary>
        /// <param name="specParams">Đối tượng tham số</param>
        /// <returns>
        /// 1 - Danh sách đặt phòng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetListAsync(BookingSpecParams specParams);
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
        /// Từ chối đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <param name="rejectionReason">Lý do từ chối</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> RefuseAsync(string id, string rejectionReason);
        /// <summary>
        /// Chấp nhận đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> AcceptAsync(string id);
        /// <summary>
        /// Cập nhật đặt phòng
        /// </summary>
        /// <param name="id">Id đặt phòng</param>
        /// <param name="bookingUpdateDto">Đặt phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, BookingUpdateDto bookingUpdateDto);
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
