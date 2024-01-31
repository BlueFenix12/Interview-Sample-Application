using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Queries.SingleTruck;

public static class GetTruck
{
    public sealed class Query : IQuery<Truck>
    {
        public Guid Id { get; set; }
    }
}