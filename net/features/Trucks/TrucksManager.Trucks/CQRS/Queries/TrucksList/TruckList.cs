using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Queries.TrucksList;

public static class TrucksList
{
    public sealed class Query : IQuery<List<Truck>>
    {
    }
}