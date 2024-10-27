using Core.Dtos;
using Core.Params;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
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
        /// <param name="files">Danh sách ảnh của phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(RoomDto roomDto, IFormFile[]? files);
    }
}
