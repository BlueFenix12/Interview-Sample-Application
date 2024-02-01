using System.Linq.Expressions;
using Ardalis.Result;
using MediatR;
using TrucksManager.Trucks.Domain;
using TrucksManager.Trucks.Infrastructure;
using TrucksManager.Trucks.Mappers;
using TrucksManager.Trucks.Models;

namespace TrucksManager.Trucks.CQRS.Queries.SearchTrucks;

public class SearchTrucksQueryHandler : IRequestHandler<SearchTrucks.Query, Result<List<SearchTrucks.QueryResult>>>
{
    private readonly ITrucksRepository repository;

    public SearchTrucksQueryHandler(ITrucksRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<List<SearchTrucks.QueryResult>>> Handle(SearchTrucks.Query request, CancellationToken cancellationToken)
    {
        var pageSize = request.PagingOptions.PageSize ?? 10;
        var pageNumber = request.PagingOptions.PageNumber ?? 1;
        var numberToSkip = (pageNumber - 1) * pageSize;

        var param = Expression.Parameter(typeof(Truck), "t");
        Expression? expressionBuilder = null;

        if (!string.IsNullOrEmpty(request.Code))
        {
            var expression = Expression.Call(
                Expression.Property(param, nameof(Truck.Code)),
                "Contains",
                Type.EmptyTypes,
                Expression.Constant(request.Code));
            expressionBuilder = expressionBuilder == null
                ? expression
                : Expression.AndAlso(expressionBuilder, expression);
        }
        
        if (!string.IsNullOrEmpty(request.Name))
        {
            var expression = Expression.Call(
                Expression.Property(param, nameof(Truck.Name)),
                "Contains",
                Type.EmptyTypes,
                Expression.Constant(request.Name));
            expressionBuilder = expressionBuilder == null
                ? expression
                : Expression.AndAlso(expressionBuilder, expression);
        }
        
        if (!string.IsNullOrEmpty(request.Description))
        {
            var expression = Expression.Call(
                Expression.Property(param, nameof(Truck.Description)),
                "Contains",
                Type.EmptyTypes,
                Expression.Constant(request.Description));
            expressionBuilder = expressionBuilder == null
                ? expression
                : Expression.AndAlso(expressionBuilder, expression);
        }
        
        if (request.Status is not null)
        {
            var expression = Expression.Equal(
                Expression.Property(param, nameof(Truck.Status)),
                Expression.Constant(request.Status.Value, typeof(TruckStatus)));
            expressionBuilder = expressionBuilder == null
                ? expression
                : Expression.AndAlso(expressionBuilder, expression);
        }

        var searchExpression = Expression.Lambda<Func<Truck, bool>>(expressionBuilder, param);


        Expression<Func<Truck, string>> sortingExpression = null;
        if (request.SortingOptions is not null &&
            request.SortingOptions.SortBy is not null &&
            request.SortingOptions.SortBy.Value != SortableTruckFields.None)
        {
            sortingExpression = request.SortingOptions.SortBy switch
            {
                SortableTruckFields.Code => x => x.Code,
                SortableTruckFields.Name => x => x.Name,
                SortableTruckFields.Description => x => x.Description,
                SortableTruckFields.Status => x => x.Status.ToString(),
                _ => x => x.Code
            };
        }

        var searchTrucksResult = await this.repository.SearchTrucks(
            searchExpression,
            sortingExpression,
            request.SortingOptions.SortDirection,
            numberToSkip,
            pageSize,
            cancellationToken);

        if (!searchTrucksResult.IsSuccess)
        {
            return Result.Error(searchTrucksResult.Errors.ToArray());
        }

        var queryResult = searchTrucksResult.Value
            .Select(x => new SearchTrucks.QueryResult(x))
            .ToList();

        return Result.Success(queryResult);
    }
    
}