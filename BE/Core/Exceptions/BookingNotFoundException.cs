namespace Core.Exceptions;

public sealed class BookingNotFoundException : NotFoundException
{
    public BookingNotFoundException(string id)
        : base($"Không tìm thấy đơn đặt phòng có id: {id} trong hệ thống!")
    { }
}
