using Domain.Abstractions;

namespace Domain.Entities
{
    public class EntityBase : IIdentifiable
    {
        public Guid Id { get; set; } = default!;
    }
}
