namespace Core.Exceptions;

public sealed class PostNotFoundException : NotFoundException
{
    public PostNotFoundException(string id)
        : base($"Không tìm thấy bài đăng có id: {id} trong hệ thống!")
    { }
}
