using MediatR;
using Url.Domain;

namespace Url.Application;

public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
{
    private readonly IShortUrlRepository _shortUrlRepository;

    public CreateShortUrlCommandHandler(IShortUrlRepository shortUrlRepository)
    {
        _shortUrlRepository = shortUrlRepository;
    }
    public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        string shortCode = Guid.NewGuid().ToString("N")[7..];

        ShortUrl shortUrl = new()
        {
            OriginalUrl = Url.Domain.Url.Create(request.originalUrl),
            ShortCode = shortCode,
            UserId = request.userId
        };

        await _shortUrlRepository.AddAsync(shortUrl, cancellationToken);

        return shortCode;
    }
}
