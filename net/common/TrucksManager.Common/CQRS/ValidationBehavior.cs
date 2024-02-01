using Ardalis.Result;
using FluentValidation;
using MediatR;

namespace TrucksManager.Common.CQRS;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!this.validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            this.validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
        
        var validationFailures = validationResults
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors);

        if (validationFailures.Any())
        {
            var errors = validationFailures
                .Select(x => new ValidationError { Identifier = x.PropertyName, ErrorMessage = x.ErrorMessage })
                .ToList();
            
            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TResponse).GetGenericArguments()[0];
                var invalidMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result.Invalid), new[] { typeof(List<ValidationError>) });
            
                if (invalidMethod != null)
                {
                    return (TResponse)invalidMethod.Invoke(null, new object[] { errors });
                }
            }
            else if (typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Invalid(errors);
            }
            else
            {
                throw new ValidationException(validationFailures);
            }
        }

        return await next();
    }
}