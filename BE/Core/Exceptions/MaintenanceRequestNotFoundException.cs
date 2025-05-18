namespace Core;

public sealed class MaintenanceRequestNotFoundException : NotFoundException
{
    public MaintenanceRequestNotFoundException(string id)
        : base($"Không tìm thấy yêu cầu bảo trì có id: {id} trong hệ thống!")
    { }
}
