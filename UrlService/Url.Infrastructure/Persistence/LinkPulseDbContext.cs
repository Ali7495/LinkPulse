using Microsoft.EntityFrameworkCore;
using Url.Domain;

namespace Url.Infrastructure;

public class LinkPulseDbContext : DbContext
{
    public DbSet<ShortUrl> ShortUrls => Set<ShortUrl>();

    public LinkPulseDbContext(DbContextOptions<LinkPulseDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortUrl>(s =>
        {
            s.HasKey(e=> e.Id);

            s.Property(e=> e.ShortCode).IsRequired().HasMaxLength(20);

            s.OwnsOne(e=> e.OriginalUrl, url =>
            {
                url.Property(u=> u.Value).HasColumnName("OriginalUrl").IsRequired();
            });
        });
    }
}
