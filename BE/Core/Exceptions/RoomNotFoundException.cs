namespace Core;

public sealed class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(string id)
        : base($"Không tìm thấy phòng có id: {id} trong hệ thống!")
    { }
}
