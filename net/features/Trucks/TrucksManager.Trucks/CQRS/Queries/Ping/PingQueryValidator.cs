using FluentValidation;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class PingQueryValidator : AbstractValidator<PingQuery>
{
    public PingQueryValidator()
    {
        this.RuleFor(x => x.Value).NotEmpty();
    }
}