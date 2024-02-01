using MediatR;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class PingRequestHandler : IRequestHandler<PingQuery, string>
{
    public Task<string> Handle(PingQuery request, CancellationToken cancellationToken)
    {
        const string result = "Pong";
        return Task.FromResult<string>(result);
    }
}