using Core.Dtos;
using Core.Params;
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
        /// Lấy danh sách thông tin đặt lịch bằng specification
        /// </summary>
        /// <param name="specParams">Đối tượng tham số</param>
        /// <returns>
        /// 1 - Danh sách đặt lịch
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetListAsync(ReservationSpecParams specParams);
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
        /// Từ chối đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <param name="rejectionReason">Lý do từ chối</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> RefuseAsync(string id, string rejectionReason);
        /// <summary>
        /// Chấp nhận đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> AcceptAsync(string id);
        /// <summary>
        /// Cập nhật đặt lịch
        /// </summary>
        /// <param name="id">Id đặt lịch</param>
        /// <param name="reservationUpdateDto">Đặt lịch</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, ReservationUpdateDto reservationUpdateDto);
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
