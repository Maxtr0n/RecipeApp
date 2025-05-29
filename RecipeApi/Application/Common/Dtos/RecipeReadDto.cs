namespace Application.Common.Dtos;

public class RecipeReadDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public List<string> Ingredients { get; set; } = [];

    public string Description { get; set; } = default!;

    public List<string> Images { get; set; } = [];

    public string AuthorId { get; set; } = default!;
}