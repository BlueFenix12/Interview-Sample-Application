using System.Text.Json.Serialization;
using Ardalis.Result;
using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Commands;

public static class AddTruck
{
    public sealed class Command : ICommand<CommandResponse>
    {
        public string Code { get; set; }

        public string Name { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TruckStatus Status { get; set; }

        public string Description { get; set; }
    }
    
    public sealed class CommandResponse
    {
        public Guid Id { get; set; }
    }
}