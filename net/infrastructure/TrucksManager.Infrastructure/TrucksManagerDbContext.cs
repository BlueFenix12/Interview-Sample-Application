using Microsoft.EntityFrameworkCore;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Infrastructure;

public class TrucksManagerDbContext : DbContext
{
    public TrucksManagerDbContext(DbContextOptions<TrucksManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Truck> Trucks { get; set; }
}