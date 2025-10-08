namespace Application.Common.Dtos;

public class RecipeCreateDto
{
    public string Title { get; set; } = default!;

    public List<string> Ingredients { get; set; } = [];

    public required string Instructions { get; set; }

    public string Description { get; set; } = null!;

    public int Servings { get; set; }

    public int CookingTimeInMinutes { get; set; }

    public int PreparationTimeInMinutes { get; set; }

    public List<string> ImageUrls { get; set; } = [];
}