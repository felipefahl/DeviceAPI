using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace DeviceAPI.Middlewares
{
    public class ConfigureExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConfigureExceptionMiddleware> _logger;

        public ConfigureExceptionMiddleware(RequestDelegate next, ILogger<ConfigureExceptionMiddleware> logger)
        {
            _logger = logger;
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
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                context.Response.StatusCode,
                Message = "Server Was Unable To Process The Request" + " - " + exception.Message,
                TraceId = context.TraceIdentifier
            }));
        }
    }
}
