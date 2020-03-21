using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<ExceptionMiddleware>();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An Error Occurred: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errorMessage = ErrorMessageFactory.Build(exception);
            errorMessage.RequestId = context.Response.Headers["bidigo-rid"];
            context.Response.StatusCode = errorMessage.StatusCode;

            return context.Response.WriteAsync(errorMessage.ToString());
        }
    }
}
