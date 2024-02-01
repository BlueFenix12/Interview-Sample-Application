using System.Diagnostics;
using Ardalis.Result;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace TrucksManager.Common.CQRS.Handlers;

public class GlobalRequestExceptionHandler<TRequest, TResponse, TException>
    : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TRequest : IRequest<TResponse>
        where TException : Exception
{
    private readonly ILogger<GlobalRequestExceptionHandler<TRequest, TResponse, TException>> logger;

    public GlobalRequestExceptionHandler(ILogger<GlobalRequestExceptionHandler<TRequest, TResponse, TException>> logger)
    {
        this.logger = logger;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
        CancellationToken cancellationToken)
    {
        var ex = exception.Demystify();
        this.logger.LogError(ex, "[{name}] Something went wrong while handling request of type <{requestTypeNamespace}.{requestType}>",
            typeof(GlobalRequestExceptionHandler<TRequest, TResponse, TException>).Name,
            typeof(TRequest).Namespace,
            typeof(TRequest).Name);

        TResponse response = default;
        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];
            var errorMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result.Error), new[] { typeof(string[]) });
            
            if (errorMethod != null)
            {
                var parameters = new[] { "A server error occured", "" };
                response = (TResponse)errorMethod.Invoke(null, new object[]{ parameters });
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            response = (TResponse)(object)Result.Error("A server error occured");
        }
        else
        {
            response = default;
        }
        
        state.SetHandled(response);
        
        return Task.CompletedTask;
    }
}