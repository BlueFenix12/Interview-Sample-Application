using TrucksManager.Common.CQRS;

namespace TrucksManager.Trucks.CQRS.Commands.DeleteTruck;

public static class DeleteTruck
{
    public sealed class Command : ICommand
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}