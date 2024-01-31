using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands;

public sealed class AddTruckCommandHandler : IRequestHandler<AddTruck.Command, Result<AddTruck.CommandResponse>>
{
    private readonly ITrucksRepository repository;

    public AddTruckCommandHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<AddTruck.CommandResponse>> Handle(AddTruck.Command request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);

        Truck newTruck = new()
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Status = request.Status,
            Description = request.Description
        };

        var result = await this.repository.AddNewTruck(newTruck, cancellationToken);
        
        return result.IsSuccess
            ? Result.Success(new AddTruck.CommandResponse { Id = result.Value })
            : Result.Error("Unable to create a truck");
    }
}