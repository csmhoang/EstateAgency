using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface ICartService
    {
        /// <summary>
        /// Lấy ra tất cả chi tiết  giỏ hàng
        /// </summary>
        /// <returns>
        /// 1 - Danh sách chi tiết giỏ hàng
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
        /// <summary>
        /// Lấy ra chi tiết giỏ hàng bằng id
        /// </summary>
        /// <param name="cartId">Id chi tiết giỏ hàng</param>
        /// <returns>
        /// 1 - chi tiết Giỏ hàng        
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string cartId);
        /// <summary>
        /// Xóa phòng khỏi giỏ
        /// </summary>
        /// <param name="cartDetailId">Id chi tiết giỏ hàng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> RemoveAsync(string cartDetailId);
        /// <summary>
        /// Thêm phòng vào giỏ
        /// </summary>
        /// <param name="cartDetailDto">Chi tiết giỏ hàng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> AppendAsync(CartDetailDto cartDetailDto);
        /// <summary>
        /// Lấy thông tin giỏ phòng theo UserId
        /// </summary>
        /// <param name="userId">Id người dùng</param>
        /// <returns>
        /// 1 - Giỏ phòng
        /// 2 - Null
        /// </returns>
        Task<Response> CartCurrent(string userId);
        /// <summary>
        /// Cập nhật giỏ phòng
        /// </summary>
        /// <param name="cartId">Id giỏ phòng</param>
        /// <param name="invoiceDto">giỏ phòng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string cartId, CartDto cartDto);
    }
}
