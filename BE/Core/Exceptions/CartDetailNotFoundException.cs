namespace Core;

public sealed class CartDetailNotFoundException : NotFoundException
{
    public CartDetailNotFoundException(string id)
        : base($"Không tìm thấy chi tiết giỏ phòng có id: {id} trong hệ thống!")
    { }
}
