using static Core.Enums.InvoiceEnums;

namespace Core.Interfaces.Business;

public interface IInvoiceService
{
    /// <summary>
    /// Lấy ra tất cả hóa đơn
    /// </summary>
    /// <returns>
    /// 1 - Danh sách hóa đơn
    /// 2 - Danh sách rỗng
    /// </returns>
    Task<Response> GetAllAsync();
    /// <summary>
    /// Lấy ra hóa đơn bằng id
    /// </summary>
    /// <param name="id">Id hóa đơn</param>
    /// <returns>
    /// 1 - Hóa đơn
    /// 2 - Null
    /// </returns>
    Task<Response> GetAsync(string id);
    /// <summary>
    /// Xóa hóa đơn bằng id
    /// </summary>
    /// <param name="id">Id hóa đơn</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> DeleteAsync(string id);
    /// <summary>
    /// Cập nhật hóa đơn
    /// </summary>
    /// <param name="id">Id hóa đơn</param>
    /// <param name="invoiceDto">Hóa đơn</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> UpdateAsync(string id, InvoiceDto invoiceDto);
    /// <summary>
    /// Phản hồi hóa đơn
    /// </summary>
    /// <param name="id">Id hóa đơn</param>
    /// <param name="status">Trạng thái hóa đơn</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> ResponseAsync(string id, StatusInvoice status);
}
