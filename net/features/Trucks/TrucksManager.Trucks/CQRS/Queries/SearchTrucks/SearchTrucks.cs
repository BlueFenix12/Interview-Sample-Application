using TrucksManager.Common.CQRS;
using TrucksManager.Common.Models;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Models;

namespace TrucksManager.Trucks.CQRS.Queries.SearchTrucks;

public static class SearchTrucks
{
    public sealed class Query : IQuery<List<QueryResult>>
    {
        public PagingOptions PagingOptions { get; set; }
        
        public SortingOptions<SortableTruckFields> SortingOptions { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public TruckStatus? Status { get; set; }

        public string? Description { get; set; }
    }

    public sealed class QueryResult
    {
        public QueryResult(Truck truck)
        {
            this.Id = truck.Id;
            this.Code = truck.Code;
            this.Name = truck.Name;
            this.Status = truck.Status;
            this.Description = truck.Description;
        }

        public Guid Id { get; } = Guid.Empty;
    
        public string Code { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public TruckStatus Status { get; } = TruckStatus.OutOfService;

        public string Description { get; } = string.Empty;
    }
}