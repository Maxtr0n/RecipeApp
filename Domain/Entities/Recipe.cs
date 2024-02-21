using Domain.Abstractions;
using SharedKernel;

namespace Domain.Entities;

public sealed class Recipe : Entity, IAggregateRoot
{
    private readonly List<string> _ingredients = new();
    private readonly List<string> _images = new();

    public Recipe(
        Guid id,
        string title,
        IEnumerable<string> ingredients,
        string description,
        IEnumerable<string> images,
        string author
        ) : base(id)
    {
        //TODO guard clauses

        Title = title;
        Description = description;
        Author = author;
        _ingredients.AddRange(ingredients);
        _images.AddRange(images);
    }

    public string Title { get; private set; } = default!;

    public IReadOnlyCollection<string> Ingredients => _ingredients;

    public string Description { get; private set; } = default!;

    public IReadOnlyCollection<string> Images => _images;

    public string Author { get; private set; } = default!;

    public void Update(
        string title,
        IEnumerable<string> ingredients,
        string description,
        IEnumerable<string> images,
        string author)
    {
        //TODO: guard clauses

        Title = title;
        Description = description;
        Author = author;
        _ingredients.Clear();
        _ingredients.AddRange(ingredients);
        _images.Clear();
        _images.AddRange(images);
    }
}
