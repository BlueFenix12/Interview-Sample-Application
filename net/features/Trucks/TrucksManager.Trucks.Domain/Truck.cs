namespace TrucksManager.Trucks.Domain;

public class Truck
{
    public Guid Id { get; set; }
    
    public string Code { get; set; }

    public string Name { get; set; }

    public TruckStatus Status { get; set; } = TruckStatus.OutOfService;

    public string Description { get; set; }
}
