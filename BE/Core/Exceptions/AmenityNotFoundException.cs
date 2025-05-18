namespace Core.Exceptions;

public sealed class AmenityNotFoundException : NotFoundException
{
    public AmenityNotFoundException(string id)
        : base($"Không tìm thấy tiện nghi có id: {id} trong hệ thống!")
    { }
}
