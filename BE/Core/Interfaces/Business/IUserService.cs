using Core.Dtos;
using Core.Entities;
using Core.Params;
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
        /// Lấy ra chi tiết người dùng bằng id
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <returns>
        /// 1 - Người dùng
        /// 2 - Null
        /// </returns>
        Task<Response> GetDetailAsync(string id);
        /// <summary>
        /// Lấy danh sách thông tin người dùng bằng specification
        /// </summary>
        /// <param name="specParams">Đối tượng tham số</param>
        /// <returns>
        /// 1 - Danh sách người dùng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetListAsync(UserSpecParams specParams);
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
        /// Lấy danh sách gợi ý cho bộ tìm kiếm
        /// </summary>
        /// <returns>
        /// Danh sách option gồm (fullname, address)
        /// </returns>
        Task<Response> GetSearchOptionsAsync();
        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="id">Id người dùng</param>
        /// <param name="userUpdateDto">Người dùng</param>
        /// <param name="file">Avatar (Nullable)</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, UserUpdateDto? userUpdateDto, IFormFile? file);
    }
}
