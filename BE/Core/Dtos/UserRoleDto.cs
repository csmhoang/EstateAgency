﻿namespace Core;

public record UserRoleDto
{
    public UserDto? User { get; set; }
    public RoleDto? Role { get; set; }
}
