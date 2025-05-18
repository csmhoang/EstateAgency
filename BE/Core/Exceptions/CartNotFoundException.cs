namespace Core;

public sealed class CartNotFoundException : NotFoundException
{
    public CartNotFoundException(string id)
        : base($"Không tìm thấy giỏ phòng có id: {id} trong hệ thống!")
    { }
}
