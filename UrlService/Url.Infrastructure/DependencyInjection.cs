using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Url.Domain;

namespace Url.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<LinkPulseDbContext>(options=> options.UseNpgsql(connectionString));

        services.AddScoped<IShortUrlRepository,ShortUrlRepository>();

        return services;
    }
}
