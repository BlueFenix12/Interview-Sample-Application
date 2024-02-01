using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Commands.UpdateTruck;

public static class UpdateTruck
{
    public class BodyCommand
    {
        public string Name { get; init; } = string.Empty;
        
        public TruckStatus Status { get; init; } = TruckStatus.OutOfService;

        public string Description { get; init; } = string.Empty;
    }

    public sealed class Command : BodyCommand, ICommand
    {
        public Guid Id { get; init; } = Guid.Empty;
    }
}