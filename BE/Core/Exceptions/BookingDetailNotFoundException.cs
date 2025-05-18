namespace Core;

public sealed class BookingDetailNotFoundException : NotFoundException
{
    public BookingDetailNotFoundException(string id)
        : base($"Không tìm thấy chi tiết đơn đặt phòng có id: {id} trong hệ thống!")
    { }
}
