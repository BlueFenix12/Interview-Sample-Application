using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands.UpdateTruck;

public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruck.Command, Result>
{
    private readonly ITrucksRepository repository;

    public UpdateTruckCommandHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> Handle(UpdateTruck.Command request, CancellationToken cancellationToken)
    {
        Truck updatedTruck = new()
        {
            Id = request.Id,
            Name = request.Name,
            Status = request.Status,
            Description = request.Description
        };
        var result = await this.repository.UpdateTruckAsync(updatedTruck, cancellationToken);
        return result;
    }
}