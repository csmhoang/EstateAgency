using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface IRoomService
    {
        /// <summary>
        /// Lấy ra tất cả phòng
        /// </summary>
        /// <returns>
        /// 1 - Danh sách phòng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
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
        /// Xóa phòng bằng id
        /// </summary>
        /// <param name="id">Id phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> DeleteAsync(string id);
        /// <summary>
        /// Cập nhật phòng
        /// </summary>
        /// <param name="id">Id phòng</param>
        /// <param name="roomDto">Phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, RoomDto roomDto);
        /// <summary>
        /// Thêm phòng
        /// </summary>
        /// <param name="roomDto">Phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(RoomDto roomDto);
    }
}
