using Microsoft.EntityFrameworkCore;
using TrucksManager.Infrastructure;

namespace TrucksApi.Configuration;

public static class DatabaseConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddDbContext<TrucksManagerDbContext>(options =>
        {
            options.UseInMemoryDatabase("TrucksManager");
        });
    }
}