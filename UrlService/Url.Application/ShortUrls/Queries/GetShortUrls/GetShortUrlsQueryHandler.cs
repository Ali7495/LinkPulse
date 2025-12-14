using MediatR;
using Url.Domain;

namespace Url.Application;

public class GetShortUrlsQueryHandler : IRequestHandler<GetShortUrlsQuery, ShortUrl>
{
    private readonly IShortUrlRepository _shortUrlRepository;

    public GetShortUrlsQueryHandler(IShortUrlRepository shortUrlRepository)
    {
        _shortUrlRepository = shortUrlRepository;
    }

    public async Task<ShortUrl> Handle(GetShortUrlsQuery request, CancellationToken cancellationToken)
    {
        return await _shortUrlRepository.GetByCodeAsync(request.shortCode,cancellationToken);
    }
}
