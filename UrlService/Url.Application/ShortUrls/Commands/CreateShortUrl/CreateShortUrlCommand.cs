using MediatR;

namespace Url.Application;

public record CreateShortUrlCommand(string originalUrl, Guid? userId) : IRequest<string>;
