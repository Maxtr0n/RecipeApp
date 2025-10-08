namespace Application.Common.Dtos;

public class RecipeUpdateDto
{
    public string Title { get; set; } = null!;

    public List<string> Ingredients { get; set; } = [];

    public string Instructions { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Servings { get; set; }

    public int CookingTimeInMinutes { get; set; }

    public int PreparationTimeInMinutes { get; set; }

    public List<string> ImageUrls { get; set; } = [];
}