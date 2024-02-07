using Domain.Abstractions;

namespace Domain.Entities
{
    public class Recipe : BaseEntity, IAggregateRoot
    {
        public string Title { get; set; } = default!;

        public List<string> Ingredients { get; set; } = [];

        public string Description { get; set; } = default!;

        public List<string> Images { get; set; } = [];

        public string Author { get; set; } = default!;
    }
}
