using Application.Common.Abstractions.Repositories;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Abstractions;
using Infrastructure.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(RecipeDbContext context) : RepositoryBase<T>(context), IRepository<T> where T : class, IAggregateRoot { }