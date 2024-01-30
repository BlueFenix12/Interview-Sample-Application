using Ardalis.Result;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.DataAccess;

public class TrucksRepository : ITrucksRepository
{
    public Task<Result> IsTruckCodeValid(string code, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Conflict());
    }

    public Task<Result<Guid>> AddNewTruck(Truck newTruck, CancellationToken cancellationToken)
    {
        return Task.FromResult<Result<Guid>>(Guid.NewGuid());
    }
}