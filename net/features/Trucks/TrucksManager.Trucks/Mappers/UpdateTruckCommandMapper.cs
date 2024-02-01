using TrucksManager.Trucks.CQRS.Commands.UpdateTruck;

namespace TrucksManager.Trucks.Mappers;

public static class UpdateTruckCommandMapper
{
    public static UpdateTruck.Command MapToUpdateTruckCommand(this UpdateTruck.BodyCommand command, Guid id)
    {
        return new UpdateTruck.Command()
        {
            Id = id,
            Name = command.Name,
            Status = command.Status,
            Description = command.Description
        };
    }
}