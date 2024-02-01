using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Commands.UpdateTruck;

public static class UpdateTruck
{
    public class BodyCommand
    {
        public string Name { get; set; } = string.Empty;
        
        public TruckStatus Status { get; set; } = TruckStatus.OutOfService;

        public string Description { get; set; } = string.Empty;
    }

    public sealed class Command : BodyCommand, ICommand
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}