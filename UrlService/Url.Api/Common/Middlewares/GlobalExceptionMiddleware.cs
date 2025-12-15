using System.ComponentModel.DataAnnotations;
using Serilog.Context;

namespace Url.Api;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        =>  _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string traceId = context.TraceIdentifier;

        using(LogContext.PushProperty("TraceId",traceId))
        using(LogContext.PushProperty("Path",context.Request.Path.Value))
        using(LogContext.PushProperty("Method", context.Request.Method))
        {
            try
            {
                await next(context);
            }
            catch(ValidationException ex)
            {
                
            }
        }
    }
}
