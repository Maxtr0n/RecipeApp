namespace Domain.Abstractions;

public interface IGenericRepository<T> where T : class, IAggregateRoot
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync(bool tracking = true);
    void UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
}