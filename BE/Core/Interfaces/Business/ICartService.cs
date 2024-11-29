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
        /// <param name="id">Id chi tiết giỏ hàng</param>
        /// <returns>
        /// 1 - chi tiết Giỏ hàng        
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string id);
        /// <summary>
        /// Xóa phòng khỏi giỏ
        /// </summary>
        /// <param name="id">Id chi tiết giỏ hàng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> RemoveAsync(string id);
        /// <summary>
        /// Thêm phòng vào giỏ
        /// </summary>
        /// <param name="cartDetailDto">Chi tiết giỏ hàng</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> AppendAsync(CartDetailDto cartDetailDto);
    }
}
