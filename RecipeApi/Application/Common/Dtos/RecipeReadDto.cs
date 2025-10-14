namespace Application.Common.Dtos;

public class RecipeReadDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public List<IngredientReadDto> Ingredients { get; set; } = [];

    public string Instructions { get; set; } = default!;

    public string? Description { get; set; }

    public int Servings { get; set; }

    public int CookingTimeInMinutes { get; set; }

    public int PreparationTimeInMinutes { get; set; }

    public List<string> Images { get; set; } = [];

    public string AuthorId { get; set; } = default!;

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset UpdatedAtUtc { get; set; }
}