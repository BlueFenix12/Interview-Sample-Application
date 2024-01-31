﻿using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
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
                ? Result.Success()
                : Result.Conflict();
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
}