namespace Core;
public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException()
        : base($"Không tìm thấy người dùng trên hệ thống!")
    { }
}
