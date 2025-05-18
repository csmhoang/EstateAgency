namespace Core;

public class RoleNotFoundException : NotFoundException
{
    public RoleNotFoundException(string role)
        : base($"Không tồn tại role: {role} trong hệ thống!")
    { }
}
