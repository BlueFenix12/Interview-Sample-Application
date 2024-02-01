using TrucksManager.Common.CQRS;
using TrucksManager.Trucks.Domain;

namespace TrucksManager.Trucks.CQRS.Queries.TrucksList;

public static class TrucksList
{
    public sealed class Query : IQuery<List<QueryResult>>
    {
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
        
        public Guid Id { get;  } = Guid.Empty;
    
        public string Code { get; } = string.Empty;

        public string Name { get; } = string.Empty;

        public TruckStatus Status { get; } = TruckStatus.OutOfService;

        public string Description { get; } = string.Empty;
    }
}