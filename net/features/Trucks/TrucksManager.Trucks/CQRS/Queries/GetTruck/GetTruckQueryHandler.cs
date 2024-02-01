using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Queries.SingleTruck;

public class GetTruckQueryHandler : IRequestHandler<GetTruck.Query, Result<GetTruck.QueryResult>>
{
    private readonly ITrucksRepository repository;

    public GetTruckQueryHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<GetTruck.QueryResult>> Handle(GetTruck.Query request, CancellationToken cancellationToken)
    {
        var getTruckResult = await this.repository.GetTruckAsync(request.Id, cancellationToken);
        return getTruckResult.Status switch
        {
            ResultStatus.Ok => Result.Success(new GetTruck.QueryResult(getTruckResult.Value)),
            ResultStatus.NotFound => Result.NotFound(),
            _ => Result.Error(getTruckResult.Errors.ToArray())
        };
    }
}