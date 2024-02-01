using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands;

public sealed class AddTruckCommandHandler : IRequestHandler<AddTruck.Command, Result<AddTruck.CommandResult>>
{
    private readonly ITrucksRepository repository;
    private readonly ILogger<AddTruckCommandHandler> logger;

    public AddTruckCommandHandler(ITrucksRepository repository, ILogger<AddTruckCommandHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<Result<AddTruck.CommandResult>> Handle(AddTruck.Command request, CancellationToken cancellationToken)
    {
        Truck newTruck = new()
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Status = request.Status,
            Description = request.Description
        };

        var addTruckResult = await this.repository.AddNewTruck(newTruck, cancellationToken);
        return addTruckResult.Status switch
        {
            ResultStatus.Ok => Result.Success(new AddTruck.CommandResult { Id = addTruckResult.Value }),
            _ => Result.Error(addTruckResult.Errors.ToArray())
        };
    }
}