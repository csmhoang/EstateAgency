using Core.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface IPostService
    {
        /// <summary>
        /// Lấy ra tất cả bài đăng
        /// </summary>
        /// <returns>
        /// 1 - Danh sách bài đăng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
        /// <summary>
        /// Lấy ra bài đăng bằng id
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <returns>
        /// 1 - Bài đăng
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string id);
        /// <summary>
        /// Xóa bài đăng bằng id
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> DeleteAsync(string id);
        /// <summary>
        /// Cập nhật bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <param name="postDto">Bài đăng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, PostDto postDto);
        /// <summary>
        /// Thêm bài đăng
        /// </summary>
        /// <param name="postDto">Bài đăng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(PostDto postDto);
    }
}
