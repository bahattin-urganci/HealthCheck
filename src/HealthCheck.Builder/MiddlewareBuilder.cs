using HealthCheck.ExceptionHandler;
using HealthCheck.Logger;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Builder
{
    public static class MiddleWareBuilder
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder) => builder.UseMiddleware<RequestResponseLoggingMiddleware>();        
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();

    }
}
