using MediatR;
using Microsoft.Extensions.Logging;

namespace TrucksManager.Common.CQRS;

public class LoggerPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ILogger<LoggerPipelineBehavior<TRequest, TResponse>> logger;

    public LoggerPipelineBehavior(ILogger<LoggerPipelineBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("[{namespace}.{name}] - Processing started", typeof(TRequest).Namespace, typeof(TRequest).Name);
        return await next();
    }
}