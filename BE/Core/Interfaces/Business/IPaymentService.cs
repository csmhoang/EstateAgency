namespace Core.Interfaces.Business;

public interface IPaymentService
{
    /// <summary>
    /// Lấy ra tất cả thanh toán
    /// </summary>
    /// <returns>
    /// 1 - Danh sách thanh toán
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetAllAsync();
    /// <summary>
    /// Lấy ra thanh toán bằng id
    /// </summary>
    /// <param name="id">Id thanh toán</param>
    /// <returns>
    /// 1 - Thanh toán
    /// 2 - Null
    /// </returns>
    Task<Response> GetAsync(string id);
    /// <summary>
    /// Xóa thanh toán bằng id
    /// </summary>
    /// <param name="id">Id thanh toán</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> DeleteAsync(string id);
    /// <summary>
    /// Cập nhật thanh toán
    /// </summary>
    /// <param name="id">Id thanh toán</param>
    /// <param name="paymentDto">Thanh toán</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> UpdateAsync(string id, PaymentDto paymentDto);
    /// <summary>
    /// Thêm thanh toán
    /// </summary>
    /// <param name="invoiceId">Id hóa đơn</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> InsertAsync(string invoiceId);
}
