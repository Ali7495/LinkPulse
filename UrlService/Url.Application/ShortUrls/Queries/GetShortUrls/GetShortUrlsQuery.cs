using MediatR;
using Url.Domain;

namespace Url.Application;

public record GetShortUrlsQuery(string shortCode) : IRequest<ShortUrl?>;
