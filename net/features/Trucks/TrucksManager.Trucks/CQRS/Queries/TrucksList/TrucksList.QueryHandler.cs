using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.CQRS.Queries.TrucksList;

public static partial class TrucksList
{
    public sealed class QueryHandler : IRequestHandler<Query, Result<List<Truck>>>
    {
        private readonly ITrucksRepository repository;

        public QueryHandler(ITrucksRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<List<Truck>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await this.repository.GetAllTrucksAsync(cancellationToken);
            return result;
        }
    }
}