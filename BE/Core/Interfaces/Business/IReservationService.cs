using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface IReservationService
    {
        /// <summary>
        /// Lấy ra tất cả đặt lịch
        /// </summary>
        /// <returns>
        /// 1 - Danh sách đặt lịch
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
        /// <summary>
        /// Lấy ra đặt lịch bằng id
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <returns>
        /// 1 - Đặt lịch
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string id);
        /// <summary>
        /// Xóa đặt lịch bằng id
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> DeleteAsync(string id);
        /// <summary>
        /// Cập nhật đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <param name="reservationDto">Đặt lịch</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, ReservationDto reservationDto);
        /// <summary>
        /// Thêm đặt lịch
        /// </summary>
        /// <param name="reservationDto">Đặt lịch</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(ReservationDto reservationDto);
    }
}
