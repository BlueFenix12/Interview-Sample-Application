using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands.DeleteTruck;

public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruck.Command, Result>
{
    private readonly ITrucksRepository repository;

    public DeleteTruckCommandHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> Handle(DeleteTruck.Command request, CancellationToken cancellationToken)
    {
        var truckResult = await this.repository.GetTruckAsync(request.Id, cancellationToken);
        if (!truckResult.IsSuccess)
        {
            return truckResult.Status switch
            {
                ResultStatus.NotFound => Result.NotFound(),
                _ => Result.Error()
            };
        }

        var deleteResult = await this.repository.DeleteTruckAsync(request.Id, cancellationToken);
        return deleteResult;
    }
}