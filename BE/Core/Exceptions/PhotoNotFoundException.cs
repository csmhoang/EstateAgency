namespace Core;

public sealed class PhotoNotFoundException: NotFoundException
{
    public PhotoNotFoundException(string id)
        : base($"Không tìm thấy ảnh có id: {id} trong hệ thống!")
    { }
}
