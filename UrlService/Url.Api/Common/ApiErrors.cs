namespace Url.Api;

public sealed record ApiErrors(string Message, string? Code, string? TraceId, object? Details);
