using System.Net;

namespace Core.Exceptions
{
    public class CustomizeException : Exception
    {
        private string _message;
        private int _statusCode;

        public CustomizeException(string message, int? statusCode = null)
        {
            _message = message;
            _statusCode = statusCode ?? (int)HttpStatusCode.BadRequest;
        }
        public override string Message => _message;
        public int StatusCode => _statusCode;
    }
}
