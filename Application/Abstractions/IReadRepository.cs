using Ardalis.Specification;
using Domain.Abstractions;

namespace Application.Abstractions;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}

