using Ardalis.Result;
using MediatR;

namespace TrucksManager.Common.CQRS;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}