namespace Url.Domain;

public interface IShortUrlRepository
{
    Task AddAsync(ShortUrl shortUrl, CancellationToken cancellationToken = default);
    Task<ShortUrl?> GetByCodeAsync(string shortCode, CancellationToken cancellationToken = default); 
}
