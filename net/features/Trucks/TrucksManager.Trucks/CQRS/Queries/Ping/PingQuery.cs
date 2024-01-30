using Ardalis.Result;
using FluentValidation;
using MediatR;
using TrucksManager.Common;
using TrucksManager.Common.CQRS;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public static class Ping
{
    public sealed class Query : IQuery<string>
    {
        public string Value { get; set; }
    }
    
    public sealed class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            this.RuleFor(x => x.Value).NotEmpty().MaximumLength(3);
        }
    }
    
    public sealed class PingQueryRequestHandler : IRequestHandler<Query, Result<string>>
    {
        public Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            const string value = "Pong";
            return Task.FromResult(Result<string>.Success(value));
        }
    }
}