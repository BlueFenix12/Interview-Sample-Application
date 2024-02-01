using Microsoft.EntityFrameworkCore;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Infrastructure;

public class TrucksManagerDbContext : DbContext
{
    public TrucksManagerDbContext(DbContextOptions<TrucksManagerDbContext> options)
        : base(options)
    {
        LoadSampleData();
    }

    public DbSet<Truck> Trucks { get; set; }

    private void LoadSampleData()
    {
        if (this.Trucks.Any())
        {
            return;
        }

        this.Trucks.Add(new Truck { Id = Guid.NewGuid(), Code = "ASDA1234", Name = "Test 0", Status = TruckStatus.OutOfService});
        this.Trucks.Add(new Truck { Id = Guid.NewGuid(), Code = "ASDA4321", Name = "Test 1", Status = TruckStatus.OutOfService});
        this.Trucks.Add(new Truck { Id = Guid.NewGuid(), Code = "ADSA1234", Name = "Test 2", Status = TruckStatus.OutOfService});
        this.Trucks.Add(new Truck { Id = Guid.NewGuid(), Code = "ADSA4321", Name = "Test 3", Status = TruckStatus.OutOfService});
        this.SaveChanges();
    }
}