using Ardalis.GuardClauses;
using Domain.Abstractions;
using SharedKernel;

namespace Domain.Entities;

public class Recipe : Entity, IAggregateRoot
{
    public Recipe(
        string title,
        string ingredients,
        string instructions,
        string description,
        int preparationTimeInMinutes,
        int cookingTimeInMinutes,
        int servings,
        string? imageUrls,
        string authorId
    ) : base(Guid.NewGuid())
    {
        GuardAgainstInvalidInput(title, ingredients, instructions, preparationTimeInMinutes, cookingTimeInMinutes,
            servings, authorId);
        Title = title;
        Ingredients = ingredients;
        Instructions = instructions;
        Description = description;
        PreparationTimeInMinutes = preparationTimeInMinutes;
        CookingTimeInMinutes = cookingTimeInMinutes;
        Servings = servings;
        ImageUrls = imageUrls;
        AuthorId = authorId;
    }

    public string Title { get; private set; }

    public string Ingredients { get; private set; }

    public string Instructions { get; private set; }

    public string? Description { get; private set; }

    public int PreparationTimeInMinutes { get; private set; }

    public int CookingTimeInMinutes { get; private set; }

    public int Servings { get; private set; }

    public string? ImageUrls { get; private set; }

    public string AuthorId { get; private set; }

    private static void GuardAgainstInvalidInput(string title, string ingredients, string instructions,
        int preparationTimeInMinutes, int cookingTimeInMinutes, int servings, string authorId)
    {
        Guard.Against.NullOrEmpty(title);
        Guard.Against.StringTooShort(title, 3);
        Guard.Against.StringTooLong(title, 100);

        Guard.Against.NullOrEmpty(ingredients);

        Guard.Against.NullOrEmpty(instructions);
        Guard.Against.StringTooShort(instructions, 3);
        Guard.Against.StringTooLong(instructions, 5000);

        Guard.Against.Null(preparationTimeInMinutes);
        Guard.Against.Null(cookingTimeInMinutes);
        Guard.Against.Null(servings);

        Guard.Against.NullOrEmpty(authorId);
    }

    public void Update(
        string title,
        string ingredients,
        string instructions,
        string description,
        int preparationTimeInMinutes,
        int cookingTimeInMinutes,
        int servings,
        string? imageUrls)
    {
        GuardAgainstInvalidInput(title, ingredients, instructions, preparationTimeInMinutes, cookingTimeInMinutes,
            servings, AuthorId);

        Title = title;
        Ingredients = ingredients;
        Instructions = instructions;
        ImageUrls = imageUrls;
        Description = description;
        PreparationTimeInMinutes = preparationTimeInMinutes;
        CookingTimeInMinutes = cookingTimeInMinutes;
        Servings = servings;
    }

    public void UpdateAuthor(string authorId)
    {
        Guard.Against.NullOrEmpty(authorId);
        AuthorId = authorId;
    }
}