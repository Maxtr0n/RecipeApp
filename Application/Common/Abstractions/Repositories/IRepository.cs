using Ardalis.Specification;
using Domain.Abstractions;

namespace Application.Common.Abstractions.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot { }
