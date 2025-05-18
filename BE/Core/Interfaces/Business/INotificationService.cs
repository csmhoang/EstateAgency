using static Core.Enums.NotificationEnums;

namespace Core.Interfaces.Business;

public interface INotificationService
{
    /// <summary>
    /// Phản hồi thông báo
    /// </summary>
    /// <param name="id">Id thông báo</param>
    /// <param name="status">Trạng thái</param>
    /// <returns>
    /// 1 - Thông báo thành công
    /// 2 - Ngoại lệ
    /// </returns>
    Task<Response> ResponseAsync(string id, StatusNotification status);
}
