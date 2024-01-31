using Ardalis.Result;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.Infrastructure;

public interface ITrucksRepository
{
    Task<Result> IsTruckCodeValid(string code, CancellationToken cancellationToken);
    Task<Result<Guid>> AddNewTruck(Truck newTruck, CancellationToken cancellationToken);
    Task<Result<List<Truck>>> GetAllTrucksAsync(CancellationToken cancellationToken);
    Task<Result<Truck>> GetTruckAsync(Guid requestId, CancellationToken cancellationToken);
}