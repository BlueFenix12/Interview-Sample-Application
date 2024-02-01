namespace TrucksManager.Trucks.Domain;

public class Truck
{
    public Guid Id { get; set; } = Guid.Empty;
    
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public TruckStatus Status { get; set; } = TruckStatus.OutOfService;

    public string Description { get; set; } = string.Empty;
}
