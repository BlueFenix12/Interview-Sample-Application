using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Queries.TrucksList;

public sealed class TruckListQueryHandler : IRequestHandler<TrucksList.Query, Result<List<TrucksList.QueryResult>>>
{
    private readonly ITrucksRepository repository;

    public TruckListQueryHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<TrucksList.QueryResult>>> Handle(TrucksList.Query request, CancellationToken cancellationToken)
    {
        var getAllTrucksResult = await this.repository.GetAllTrucksAsync(cancellationToken);
        return getAllTrucksResult.Status switch
        {
            ResultStatus.Ok => Result.Success(getAllTrucksResult.Value.Select(x => new TrucksList.QueryResult(x)).ToList()),
            _ => Result.Error(getAllTrucksResult.Errors.ToArray())
        };
    }
}