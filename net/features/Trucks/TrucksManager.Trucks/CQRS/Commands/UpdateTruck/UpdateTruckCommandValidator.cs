using FluentValidation;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands.UpdateTruck;

public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruck.Command>
{
    private readonly ITrucksRepository repository;

    public UpdateTruckCommandValidator(ITrucksRepository repository)
    {
        this.repository = repository;
        
        this.RuleFor(x => x.Name)
            .NotEmpty();
        
        this.RuleFor(x => x.Status)
            .IsInEnum();
        
        this.RuleFor(x => new KeyValuePair<Guid, TruckStatus>(x.Id, x.Status))
            .Cascade(CascadeMode.Stop)
            .MustAsync(this.CheckIfTruckExists)
            .WithMessage("Truck with given ID does not exist")
            .MustAsync(this.CheckIfTruckStatusIsValid)
            .WithMessage("Invalid Truck status");
    }
    
    private async Task<bool> CheckIfTruckExists(KeyValuePair<Guid, TruckStatus> idAndStatus, CancellationToken cancellationToken)
    {
        Guid truckId = idAndStatus.Key;
        var result = await this.repository.GetTruckAsync(truckId, cancellationToken);
        return result.IsSuccess;
    }
    
    private async Task<bool> CheckIfTruckStatusIsValid(KeyValuePair<Guid, TruckStatus> idAndStatus, CancellationToken cancellationToken)
    {
        Guid truckId = idAndStatus.Key;
        var truckResult = await this.repository.GetTruckAsync(truckId, cancellationToken);
        if (!truckResult.IsSuccess)
        {
            return false;
        }

        TruckStatus currentTruckStatus = truckResult.Value.Status;
        TruckStatus newTruckStatus = idAndStatus.Value;

        if (newTruckStatus == TruckStatus.OutOfService || currentTruckStatus == TruckStatus.OutOfService)
        {
            return true;
        }

        if ((currentTruckStatus == TruckStatus.Loading && newTruckStatus == TruckStatus.ToJob)
            || (currentTruckStatus == TruckStatus.ToJob && newTruckStatus == TruckStatus.AtJob)
            || (currentTruckStatus == TruckStatus.AtJob && newTruckStatus == TruckStatus.Returning))
        {
            return true;
        }

        if (currentTruckStatus == TruckStatus.Returning && newTruckStatus == TruckStatus.Loading)
        {
            return true;
        }

        return false;
    }
}