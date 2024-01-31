using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Queries.SingleTruck;

public class GetTruckQueryHandler : IRequestHandler<GetTruck.Query, Result<Truck>>
{
    private readonly ITrucksRepository repository;

    public GetTruckQueryHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<Truck>> Handle(GetTruck.Query request, CancellationToken cancellationToken)
    {
        var result = await this.repository.GetTruckAsync(request.Id, cancellationToken);
        return result;
    }
}