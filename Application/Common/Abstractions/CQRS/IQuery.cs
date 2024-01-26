using MediatR;

namespace Application.Common.Abstractions.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse> { }