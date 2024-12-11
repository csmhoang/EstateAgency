using Core.Dtos;

namespace Core.Interfaces.Business
{
    public interface IConversationService
    {
        /// <summary>
        /// Lấy ra tất cả hội thoại bằng userId
        /// </summary>
        /// <param name="userId">Id người dùng</param>
        /// <returns>
        /// 1 - Danh sách hội thoại
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllByUserIdAsync(string userId);
        /// <summary>
        /// Lấy ra cuộc hội thoại bằng id của hai người nếu không có tự tạo
        /// </summary>
        /// <param name="callerId">Id người gọi</param>
        /// <param name="otherId">Id người khác</param>
        /// <returns>Cuộc hội thoại</returns>
        Task<Response> GetByTwoUserIdAsync(string callerId, string otherId);
    }
}
