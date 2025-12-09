using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Url.Domain;

namespace Url.Infrastructure;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly LinkPulseDbContext _db;

    public ShortUrlRepository(LinkPulseDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ShortUrl shortUrl, CancellationToken cancellationToken = default)
    {
        await _db.ShortUrls.AddAsync(shortUrl,cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<ShortUrl?> GetByCodeAsync(string shortCode, CancellationToken cancellationToken = default)
    {
        return await _db.ShortUrls.FirstOrDefaultAsync(s=> s.ShortCode == shortCode, cancellationToken);
    }
}
