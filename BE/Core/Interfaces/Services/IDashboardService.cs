namespace Core;

public interface IDashboardService
{
    /// <summary>
    /// Tổng số phòng
    /// </summary>
    /// <param name="landlordId">Id chủ nhà</param>
    /// <returns>Tổng số phòng</returns>
    Task<Response> RoomCountAsync(string landlordId);
    /// <summary>
    /// Đếm số phòng còn trống
    /// </summary>
    /// <param name="landlordId">Id chủ nhà</param>
    /// <returns>Tổng số phòng còn trống</returns>
    Task<Response> RoomBlankCountAsync(string landlordId);
    /// <summary>
    /// Số người thuê theo mốc thời gian
    /// </summary>
    /// <param name="landlordId">Id chủ nhà</param>
    /// <returns>Tổng số người thuê</returns>
    Task<Response> TenantCountAsync(string landlordId);
    /// <summary>
    /// Doanh thu năm
    /// </summary>
    /// <param name="landlordId">Id chủ nhà</param>
    /// <returns>Tổng doanh thu</returns>
    Task<Response> RevenueAsync(string landlordId);
    /// <summary>
    /// Số lượt truy cập trong tháng
    /// </summary>
    /// <param name="numberOfMonth">Số tháng</param>
    /// <returns>Số lượt truy cập</returns>
    Task<Response> VisitCountAsync(int numberOfMonth);
}
