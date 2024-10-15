namespace Core.Exceptions;
public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string id)
        : base($"Không tìm thấy người dùng có id: {id} trong Database!")
    { }
}
