using TrucksManager.Common.CQRS;
using TrucksManager.Common.Models;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Models;

namespace TrucksManager.Trucks.CQRS.Queries.SearchTrucks;

public static class SearchTrucks
{
    public sealed class Query : IQuery<List<Truck>>
    {
        public PagingOptions PagingOptions { get; set; }
        
        public SortingOptions<SortableTruckFields> SortingOptions { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public TruckStatus? Status { get; set; }

        public string? Description { get; set; }
    }
}