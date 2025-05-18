namespace Core.Exceptions;

public sealed class RefreshTokenBadrequest : BadRequestException
{
    public RefreshTokenBadrequest() 
        : base("Yêu cầu không hợp lệ. Giá trị TokenDto không hợp lệ!")
    {

    }
}
