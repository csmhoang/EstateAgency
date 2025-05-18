using System.Net;

namespace Core;

public class CustomizeException : Exception
{
    private readonly int _statusCode;

    public CustomizeException(string? message, int? statusCode = null):base(message)
    {
        _statusCode = statusCode ?? (int)HttpStatusCode.InternalServerError;
    }
    public int StatusCode => _statusCode;
}
