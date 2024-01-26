using Domain.Abstractions;

namespace Domain.Entities
{
    public abstract class BaseEntity : IIdentifiable
    {
        public Guid Id { get; set; } = default!;
    }
}
