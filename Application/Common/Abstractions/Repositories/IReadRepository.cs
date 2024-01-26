using Ardalis.Specification;
using Domain.Abstractions;

namespace Application.Common.Abstractions.Repositories;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}

