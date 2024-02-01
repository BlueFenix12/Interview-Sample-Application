using FluentValidation;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands.DeleteTruck;

public class DeleteTruckCommandValidator : AbstractValidator<DeleteTruck.Command>
{
    private readonly ITrucksRepository repository;

    public DeleteTruckCommandValidator(ITrucksRepository repository)
    {
        this.repository = repository;

        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid identifier")
            .MustAsync(this.CheckIfTruckExists)
            .WithMessage("Truck with given ID does not exist");
    }

    private async Task<bool> CheckIfTruckExists(Guid id, CancellationToken cancellationToken)
    {
        var result = await this.repository.GetTruckAsync(id, cancellationToken);
        return result.IsSuccess;
    }
}