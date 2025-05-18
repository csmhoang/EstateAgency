namespace Core.Exceptions;

public sealed class NotificationNotFoundException : NotFoundException
{
    public NotificationNotFoundException(string id)
        : base($"Không tìm thấy thông báo có id: {id} trong hệ thống!")
    { }
}
