using FluentValidation;
using MediatR;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class PingRequestHandler : IRequestHandler<PingQuery, string>
{
    private readonly IValidator<PingQuery> validator;

    public PingRequestHandler(IValidator<PingQuery> validator)
    {
        this.validator = validator;
    }

    public async Task<string> Handle(PingQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(request, cancellationToken);
        return !validationResult.IsValid ? "Validation failed" : "Pong";
    }
}