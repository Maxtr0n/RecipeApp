using Ardalis.GuardClauses;
using Domain.Abstractions;
using SharedKernel;

namespace Domain.Entities;

public class Recipe : Entity, IAggregateRoot
{
    public Recipe(
        Guid id,
        string title,
        string ingredients,
        string description,
        string? images,
        string author
        ) : base(id)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.NullOrEmpty(ingredients);
        Guard.Against.NullOrEmpty(description);
        Guard.Against.NullOrEmpty(author);

        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
        Author = author;

    }

    public string Title { get; private set; }

    public string Ingredients { get; private set; }

    public string Description { get; private set; }

    public string? Images { get; private set; }

    public string Author { get; private set; }

    public void Update(
        string title,
        string ingredients,
        string description,
        string? images,
        string author)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.NullOrEmpty(ingredients);
        Guard.Against.NullOrEmpty(description);
        Guard.Against.NullOrEmpty(author);

        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
        Author = author;
    }
}
