using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Business
{
    public interface IMaintenanceRequestService
    {
        /// <summary>
        /// Lấy ra tất cả yêu cầu bảo trì
        /// </summary>
        /// <returns>
        /// 1 - Danh sách yêu cầu bảo trì
        /// 2 - Danh sách rỗng
        /// </returns>
        Task<Response> GetAllAsync();
        /// <summary>
        /// Lấy ra yêu cầu bảo trì bằng id
        /// </summary>
        /// <param name="id">Id yêu cầu bảo trì</param>
        /// <returns>
        /// 1 - Yêu cầu bảo trì
        /// 2 - Null
        /// </returns>
        Task<Response> GetAsync(string id);
        /// <summary>
        /// Xóa yêu cầu bảo trì bằng id
        /// </summary>
        /// <param name="id">Id yêu cầu bảo trì</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> DeleteAsync(string id);
        /// <summary>
        /// Cập nhật yêu cầu bảo trì
        /// </summary>
        /// <param name="id">Id yêu cầu bảo trì</param>
        /// <param name="maintenanceRequestDto">Yêu cầu bảo trì</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> UpdateAsync(string id, MaintenanceRequestDto maintenanceRequestDto);
        /// <summary>
        /// Thêm yêu cầu bảo trì
        /// </summary>
        /// <param name="maintenanceRequestDto">Yêu cầu bảo trì</param>
        /// <returns>
        /// 1 - Thông báo thành công
        /// 2 - Ngoại lệ
        /// </returns>
        Task<Response> InsertAsync(MaintenanceRequestDto maintenanceRequestDto);
    }
}
