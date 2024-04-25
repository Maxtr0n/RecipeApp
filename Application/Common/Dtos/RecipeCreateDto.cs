namespace Application.Common.Dtos;

public class RecipeCreateDto
{
    public string Title { get; set; } = default!;

    public List<string> Ingredients { get; set; } = [];

    public string Description { get; set; } = default!;

    public List<string> Images { get; set; } = [];
}
