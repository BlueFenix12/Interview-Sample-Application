using Ardalis.GuardClauses;
using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Commands;

public static partial class AddTruck
{
    public sealed class CommandHandler : IRequestHandler<Command, Result<CommandResponse>>
    {
        private readonly ITrucksRepository repository;

        public CommandHandler(ITrucksRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<CommandResponse>> Handle(Command request, CancellationToken cancellationToken)
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
                ? Result.Success(new CommandResponse { Id = result.Value })
                : Result.Error("Unable to create a truck");
        }
    }
}