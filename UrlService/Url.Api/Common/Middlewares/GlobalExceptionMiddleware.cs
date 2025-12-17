
using System.Net;
using System.Text.Json;
using FluentValidation;
using Serilog.Context;

namespace Url.Api;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string traceId = context.TraceIdentifier;

        using (LogContext.PushProperty("TraceId", traceId))
        using (LogContext.PushProperty("Path", context.Request.Path.Value))
        using (LogContext.PushProperty("Method", context.Request.Method))
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                Dictionary<string, string[]> errors = ex.Errors.GroupBy(e => e.PropertyName)
                                .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

                _logger.LogWarning(ex, "Validation Warning!");

                await WriteErrorsToJsonAsync(context, HttpStatusCode.BadRequest,
                                   message: "Validation failed.",
                                   code: "validation_error",
                                   traceId: traceId,
                                   details: errors);
            }
            catch (Exception ex)
            {
                // Unhandled -> 500
                _logger.LogError(ex, "Unhandled exception");

                await WriteErrorsToJsonAsync(context, HttpStatusCode.InternalServerError,
                    message: "Something went wrong.",
                    code: "server_error",
                    traceId: traceId);
            }
        }
    }


    private static async Task WriteErrorsToJsonAsync(HttpContext context, HttpStatusCode statusCode, string message, string code
    , string traceId, object? details = null)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var payload = new ApiErrors(message,code,traceId,details);

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            PropertyNamingPolicy
         = JsonNamingPolicy.CamelCase
        }));
    }
}
