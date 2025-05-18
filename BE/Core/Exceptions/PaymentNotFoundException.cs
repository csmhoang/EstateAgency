namespace Core;

public sealed class PaymentNotFoundException : NotFoundException
{
    public PaymentNotFoundException(string id)
        : base($"Không tìm thấy thanh toán có id: {id} trong hệ thống!")
    { }
}
