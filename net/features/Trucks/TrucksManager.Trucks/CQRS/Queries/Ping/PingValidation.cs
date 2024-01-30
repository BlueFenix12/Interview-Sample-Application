using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace TrucksManager.Trucks.CQRS.Queries.Ping;

public class ValidationQuery<T> : IRequest<Result> where T : class
{
    public T Data { get; set; }
}

public class ValidationQueryRequestHandler<T> : IRequestHandler<ValidationQuery<T>, Result> where T : class
{
    private readonly IEnumerable<IValidator<T>> validators;

    public ValidationQueryRequestHandler(IEnumerable<IValidator<T>> validators)
    {
        this.validators = validators;
    }

    public async Task<Result> Handle(ValidationQuery<T> request, CancellationToken cancellationToken)
    {
        if (!this.validators.Any())
        {
            return Result.Success();
        }
        
        var context = new ValidationContext<T>(request.Data);
        var validationFailures = await Task.WhenAll(
            this.validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        
        var errors = validationFailures
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Select(x => new ValidationError { Identifier = x.PropertyName, ErrorMessage = x.ErrorMessage })
            .ToList();

        return errors.Any() 
            ? Result.Invalid(errors) 
            : Result.Success();
    }
}