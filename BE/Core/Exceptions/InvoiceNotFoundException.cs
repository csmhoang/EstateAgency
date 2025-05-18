namespace Core;

public sealed class InvoiceNotFoundException : NotFoundException
{
    public InvoiceNotFoundException(string id)
        : base($"Không tìm thấy hóa đơn có id: {id} trong hệ thống!")
    { }
}
