namespace Core.Exceptions;

public sealed class ReservationNotFoundException : NotFoundException
{
    public ReservationNotFoundException(string id)
        : base($"Không tìm thấy lịch đặt có id: {id} trong hệ thống!")
    { }
}
