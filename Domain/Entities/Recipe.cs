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
        ApplicationUser author
    ) : this(id, title, ingredients, description, images)
    {
        GuardAgainstInvalidInput(title, ingredients, description, author);
        Author = author;
    }

    // EF Core ctor
    private Recipe(
        Guid id,
        string title,
        string ingredients,
        string description,
        string? images) : base(id)
    {
        Id = id;
        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
    }

    public string Title { get; private set; }

    public string Ingredients { get; private set; }

    public string Description { get; private set; }

    public string? Images { get; private set; }

    public ApplicationUser Author { get; private set; }

    private static void GuardAgainstInvalidInput(string title, string ingredients, string description,
        ApplicationUser author)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.StringTooShort(title, 3);
        Guard.Against.StringTooLong(title, 100);

        Guard.Against.NullOrEmpty(ingredients);

        Guard.Against.NullOrEmpty(description);
        Guard.Against.StringTooShort(description, 3);
        Guard.Against.StringTooLong(description, 5000);

        Guard.Against.Null(author);
        Guard.Against.Null(author.Id);
    }

    public void Update(
        string title,
        string ingredients,
        string description,
        string? images)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.NullOrEmpty(ingredients);
        Guard.Against.NullOrEmpty(description);

        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
    }
}