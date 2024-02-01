using System.Linq.Expressions;
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using TrucksManager.Common.Models;
using TrucksManager.Infrastructure;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.DataAccess;

public class TrucksRepository : ITrucksRepository
{
    private readonly TrucksManagerDbContext db;

    public TrucksRepository(TrucksManagerDbContext db)
    {
        this.db = db;
    }

    public async Task<Result> IsTruckCodeValid(string code, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await this.db.Trucks.AnyAsync(x => x.Code == code, cancellationToken);
            return exists
                ? Result.Conflict()
                : Result.Success();
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }
    }

    public async Task<Result<Guid>> AddNewTruck(Truck newTruck, CancellationToken cancellationToken)
    {
        try
        {
            await this.db.Trucks.AddAsync(newTruck, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }

        return Result.Success(newTruck.Id);

    }

    public async Task<Result<List<Truck>>> GetAllTrucksAsync(CancellationToken cancellationToken)
    {
        try
        {
            var trucks = await this.db.Trucks.ToListAsync(cancellationToken);
            return Result.Success(trucks);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }
    }

    public async Task<Result<Truck>> GetTruckAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var truck = await this.db.Trucks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return truck != null ? Result.Success(truck) : Result.NotFound();
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }
    }

    public async Task<Result> UpdateTruckAsync(Truck updatedTruck, CancellationToken cancellationToken)
    {
        try
        {
            var truck = await this.db.Trucks.FirstOrDefaultAsync(x => x.Id == updatedTruck.Id, cancellationToken);
            if (truck is null)
            {
                return Result.NotFound();
            }
            
            truck.Name = updatedTruck.Name;
            truck.Status = updatedTruck.Status;
            truck.Description = updatedTruck.Description;

            this.db.Entry(truck).State = EntityState.Modified;

            await this.db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }
        
        return Result.Success();
    }

    public async Task<Result> DeleteTruckAsync(Guid requestId, CancellationToken cancellationToken)
    {
        try
        {
            var truck = await this.db.Trucks.FirstOrDefaultAsync(x => x.Id == requestId, cancellationToken);
            if (truck is null)
            {
                return Result.NotFound();
            }

            this.db.Trucks.Remove(truck);
            await this.db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }
        
        return Result.Success();
    }

    public async Task<Result<List<Truck>>> SearchTrucks(
        Expression<Func<Truck, bool>> searchExpression,
        Expression<Func<Truck, string>> sortingExpression,
        SortDirection? sortDirection,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = this.db.Trucks.Where(searchExpression);
            var orderedQuery = (sortingExpression != null)
                ? ((sortDirection == SortDirection.Asc) ? query.OrderBy(sortingExpression) : query.OrderByDescending(sortingExpression))
                : query.OrderBy(x => "0");
            
            var searchResult = await orderedQuery
                .Skip(page)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return Result.Success(searchResult);
        }
        catch (Exception e)
        {
            return Result.Error(e.Message, e.StackTrace);
        }
    }
}