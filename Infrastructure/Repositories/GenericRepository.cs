using Application.Common.Abstractions.Repositories;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(RecipeDbContext context) : RepositoryBase<T>(context), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}