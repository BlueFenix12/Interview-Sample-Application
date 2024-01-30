using Ardalis.Result;
using MediatR;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class PingQueryRequestHandler : IRequestHandler<PingQuery, Result<string>>
{
    public Task<Result<string>> Handle(PingQuery request, CancellationToken cancellationToken)
    {
        const string value = "Pong";
        return Task.FromResult(Result<string>.Success(value));
    }
}