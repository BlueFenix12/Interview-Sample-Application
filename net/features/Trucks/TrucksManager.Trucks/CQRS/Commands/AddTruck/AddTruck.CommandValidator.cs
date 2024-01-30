using FluentValidation;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands;

public static partial class AddTruck
{
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        private readonly ITrucksRepository repository;
        
        public CommandValidator(ITrucksRepository repository)
        {
            this.repository = repository;

            this.RuleFor(x => x.Code)
                .NotEmpty()
                .MustAsync(CheckIfCodeIsValid)
                .WithMessage("Invalid truck code - must be unique");
            this.RuleFor(x => x.Name)
                .NotEmpty();
            this.RuleFor(x => x.Status)
                .IsInEnum();
        }

        private async Task<bool> CheckIfCodeIsValid(string code, CancellationToken cancellationToken)
        {
            var result = await this.repository.IsTruckCodeValid(code, cancellationToken);
            return result.IsSuccess;
        }
    }
}