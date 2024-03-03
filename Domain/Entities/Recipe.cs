using Ardalis.GuardClauses;
using Domain.Abstractions;
using SharedKernel;

namespace Domain.Entities;

public class Recipe : Entity, IAggregateRoot
{
    private readonly List<string> _ingredients = [];
    private readonly List<string> _images = [];

    public Recipe(
        Guid id,
        string title,
        IEnumerable<string> ingredients,
        string description,
        IEnumerable<string> images,
        string author
        ) : base(id)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.NullOrEmpty(ingredients);
        Guard.Against.NullOrEmpty(description);
        Guard.Against.NullOrEmpty(author);

        Title = title;
        Description = description;
        Author = author;
        _ingredients.AddRange(ingredients);
        _images.AddRange(images);
    }

    public string Title { get; private set; } = default!;

    public IEnumerable<string> Ingredients => _ingredients;

    public string Description { get; private set; } = default!;

    public IEnumerable<string> Images => _images;

    public string Author { get; private set; } = default!;

    public void Update(
        string title,
        IEnumerable<string> ingredients,
        string description,
        IEnumerable<string> images,
        string author)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.NullOrEmpty(ingredients);
        Guard.Against.NullOrEmpty(description);
        Guard.Against.NullOrEmpty(author);

        Title = title;
        Description = description;
        Author = author;
        _ingredients.Clear();
        _ingredients.AddRange(ingredients);
        _images.Clear();
        _images.AddRange(images);
    }
}
