using System;
using System.Net;

namespace HealthCheck.ExceptionHandler
{
    public static class ErrorMessageFactory
    {
        public static ErrorMessage Build(Exception exception)
        {
            if (exception is NotFoundException)
                return new ErrorMessage { Header = "Not Found", Message = exception.Message, StatusCode = (int)HttpStatusCode.NotFound };

            return new ErrorMessage { Header = "Internal Server Error", Message = exception.Message, StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}
