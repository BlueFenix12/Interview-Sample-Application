using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Queries.TrucksList;

public sealed class TruckListQueryHandler : IRequestHandler<TrucksList.Query, Result<List<Truck>>>
{
    private readonly ITrucksRepository repository;

    public TruckListQueryHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<Truck>>> Handle(TrucksList.Query request, CancellationToken cancellationToken)
    {
        var result = await this.repository.GetAllTrucksAsync(cancellationToken);
        return result;
    }
}