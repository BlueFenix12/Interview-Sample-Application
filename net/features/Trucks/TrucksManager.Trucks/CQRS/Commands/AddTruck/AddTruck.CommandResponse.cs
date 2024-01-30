namespace TrucksManager.Trucks.CQRS.Commands;

public static partial class AddTruck
{
    public sealed class CommandResponse
    {
        public Guid Id { get; set; }
    }
}