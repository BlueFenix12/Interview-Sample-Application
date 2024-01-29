using MediatR;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class PingQuery : IRequest<string>
{
    public string Value { get; set; }
}