namespace Application.Common.Dtos;

public class RecipeCreateDto
{
    public string Title { get; set; } = default!;

    public List<string> Ingredients { get; set; } = [];

    public required string Description { get; set; }

    public List<string> Images { get; set; } = [];

    public required Guid AuthorId { get; set; }
}
