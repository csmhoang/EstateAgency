﻿namespace Core;

public interface ILeaseService
{
    /// <summary>
    /// Lấy ra tất cả hợp đồng
    /// </summary>
    /// <returns>
    /// 1 - Danh sách hợp đồng
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetAllAsync();
    /// <summary>
    /// Lấy ra hợp đồng bằng id
    /// </summary>
    /// <param name="id">Id hợp đồng</param>
    /// <returns>
    /// 1 - Hợp đồng
    /// 2 - Null
    /// </returns>
    Task<Response> GetAsync(string id);
    /// <summary>
    /// Lấy ra hợp đồng bằng roomId
    /// </summary>
    /// <param name="roomId">Id hợp đồng</param>
    /// <returns>
    /// 1 - Hợp đồng
    /// 2 - Null
    /// </returns>
    Task<Response> GetByRoomIdAsync(string roomId);
    /// <summary>
    /// Xóa hợp đồng bằng id
    /// </summary>
    /// <param name="id">Id hợp đồng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> DeleteAsync(string id);
    /// <summary>
    /// Cập nhật hợp đồng
    /// </summary>
    /// <param name="id">Id hợp đồng</param>
    /// <param name="leaseDto">Hợp đồng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> UpdateAsync(string id, LeaseDto leaseDto);
    /// <summary>
    /// Thêm hợp đồng
    /// </summary>
    /// <param name="leaseDto">Hợp đồng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> InsertAsync(LeaseDto leaseDto);
    /// <summary>
    /// Phản hồi hợp đồng
    /// </summary>
    /// <param name="id">Id hợp đồng</param>
    /// <param name="status">Trạng thái hợp đồng</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> ResponseAsync(string id, LeaseEnums.StatusLease status);
}
