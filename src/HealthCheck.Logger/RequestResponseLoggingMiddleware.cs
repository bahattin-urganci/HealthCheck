using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Logger
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private Log _log;

        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                      .CreateLogger<RequestResponseLoggingMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != "OPTIONS")
            {


                Stopwatch stopwatch = new Stopwatch();
                _log = new Log()
                {
                    RequestId = string.IsNullOrEmpty(context.Request.Headers["bidigo-rid"].ToString()) ? Guid.NewGuid().ToString() : context.Request.Headers["bidigo-rid"].ToString(),
                    RequestTime = DateTime.UtcNow
                };
                stopwatch.Start();
                //_logger.LogInformation(await FormatRequest(context.Request));

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    context.Response.Headers.Add("bidigo-rid", _log.RequestId);
                    await _next(context);
                    stopwatch.Stop();
                    _log.ResponseTime = DateTime.UtcNow;
                    _log.Elapsed = stopwatch.ElapsedMilliseconds;

                    _log.Response = await FormatResponse(context.Response);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            //request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);


            return text;
        }
    }
}
