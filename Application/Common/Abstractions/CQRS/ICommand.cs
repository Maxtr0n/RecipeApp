using Ardalis.Result;
using MediatR;

namespace Application.Common.Abstractions.CQRS;
public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : Result { }

