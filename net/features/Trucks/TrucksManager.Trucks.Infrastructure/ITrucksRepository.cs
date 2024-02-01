using System.Linq.Expressions;
using Ardalis.Result;
using TrucksManager.Common.Models;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.Infrastructure;

public interface ITrucksRepository
{
    Task<Result> IsTruckCodeValid(string code, CancellationToken cancellationToken);
    Task<Result<Guid>> AddNewTruck(Truck newTruck, CancellationToken cancellationToken);
    Task<Result<List<Truck>>> GetAllTrucksAsync(CancellationToken cancellationToken);
    Task<Result<Truck>> GetTruckAsync(Guid requestId, CancellationToken cancellationToken);
    Task<Result> UpdateTruckAsync(Truck updatedTruck, CancellationToken cancellationToken);
    Task<Result> DeleteTruckAsync(Guid requestId, CancellationToken cancellationToken);
    Task<Result<List<Truck>>> SearchTrucks(
        Expression<Func<Truck, bool>> searchExpression,
        Expression<Func<Truck, string>>? sortingExpression,
        SortDirection? sortDirection,
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}