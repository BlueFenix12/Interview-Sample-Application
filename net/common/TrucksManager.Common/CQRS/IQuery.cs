using Ardalis.Result;
using MediatR;

namespace TrucksManager.Common.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}