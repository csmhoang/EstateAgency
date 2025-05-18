namespace Core;

public class UserExistedException : BadRequestException
{
    public UserExistedException(string username)
        : base($"Người dùng với Username: {username} đã tồn tại!")
    { }
}
