﻿using System.Net;

namespace Core.Exceptions
{
    public class CustomizeException : Exception
    {
        private readonly int _statusCode;

        public CustomizeException(string? message, int? statusCode = null):base(message)
        {
            _statusCode = statusCode ?? (int)HttpStatusCode.BadRequest;
        }
        public int StatusCode => _statusCode;
    }
}