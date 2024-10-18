using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface IUserService
    {
        /// <summary>
        /// Lấy ra tất cả người dùng
        /// </summary>
        /// <returns>
        /// 1 - Danh sách người dùng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
        /// <summary>
        /// Lấy ra người dùng bằng id
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>
        /// 1 - Người dùng
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string id);
        /// <summary>
        /// Xóa người dùng bằng id
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> DeleteAsync(string id);
        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <param name="userDto">Người dùng</param>
        /// <param name="file">Avatar (Nullable)</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, UserDto? userDto, IFormFile? file);
    }
}
