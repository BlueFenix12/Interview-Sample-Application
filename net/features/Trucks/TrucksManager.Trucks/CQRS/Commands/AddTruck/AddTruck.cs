using System.Text.Json.Serialization;
using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Commands;

public static class AddTruck
{
    public sealed class Command : ICommand<CommandResponse>
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TruckStatus Status { get; set; } = TruckStatus.OutOfService;

        public string Description { get; set; } = string.Empty;
    }
    
    public sealed class CommandResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
    }
}