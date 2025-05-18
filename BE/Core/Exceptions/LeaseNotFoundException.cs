namespace Core.Exceptions;

public sealed class LeaseNotFoundException : NotFoundException
{
    public LeaseNotFoundException(string id)
        : base($"Không tìm thấy hợp đồng có id: {id} trong hệ thống!")
    { }
}
