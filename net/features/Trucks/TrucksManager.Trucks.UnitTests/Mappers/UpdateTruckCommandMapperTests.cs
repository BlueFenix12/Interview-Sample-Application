using TrucksManager.Trucks.CQRS.Commands.UpdateTruck;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Mappers;

namespace TrucksManager.Trucks.UnitTests.Mappers;

public class UpdateTruckCommandMapperTests
{
    [Fact]
    public void MapToUpdateTruckCommand_ShouldReturnUpdateTruckCommand()
    {
        var id = Guid.NewGuid();
        var bodyCommand = new UpdateTruck.BodyCommand
        {
            Name = "Test",
            Description = "Test",
            Status = TruckStatus.OutOfService
        };

        var command = bodyCommand.MapToUpdateTruckCommand(id);

        command.Should().NotBeNull();
        command.Id.Should().Be(id);
        command.Name.Should().Be(bodyCommand.Name);
        command.Description.Should().Be(bodyCommand.Description);
        command.Status.Should().Be(bodyCommand.Status);
    }
}