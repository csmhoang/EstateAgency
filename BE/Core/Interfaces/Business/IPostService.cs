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
        /// Lấy danh sách thông tin bài đăng bằng specification
        /// </summary>
        /// <param name="specParams">Đối tượng tham số</param>
        /// <returns>
        /// 1 - Danh sách bài đăng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetListAsync(PostSpecParams specParams);
        /// <summary>
        /// Lấy danh sách thông tin bài đăng mới nhất
        /// </summary>
        /// <returns>        
        /// 1 - Danh sách bài đăng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetListRecentAsync();
        /// <summary>
        /// Lấy danh sách gợi ý cho bộ tìm kiếm
        /// </summary>
        /// <returns>
        /// Danh sách option gồm (title, room name, room address)
        /// </returns>
        Task<Response> GetSearchOptionsAsync();
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
        /// Lấy ra chi tiết bài đăng bằng id
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <returns>
        /// 1 - Bài đăng
        /// 2 - Null
        /// </returns>
        Task<Response> GetDetailAsync(string id);
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
        /// Gỡ bài đăng bằng id
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> RemoveAsync(string id);
        /// <summary>
        /// Cập nhật bài đăng
        /// </summary>
        /// <param name="id">Id bài đăng</param>
        /// <param name="postUpdateDto">Bài đăng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, PostUpdateDto postUpdateDto);
        /// <summary>
        /// Thêm bài đăng
        /// </summary>
        /// <param name="postDto">Bài đăng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(PostDto postDto);
        /// <summary>
        /// Lưu bài viết để xem lại
        /// </summary>
        /// <param name="savePostDto">Thông tin lưu</param>
        /// <param name="isSave">Lưu hoặc gỡ khỏi</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> SavePostAsync(SavePostDto savePostDto, bool isSave);
    }
}
