using Ardalis.Result;
using MediatR;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class PingQuery : IRequest<Result<string>>
{
    public string Value { get; set; }
}