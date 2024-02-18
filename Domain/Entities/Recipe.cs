using Domain.Abstractions;
using SharedKernel;

namespace Domain.Entities;

public class Recipe : BaseEntity, IAggregateRoot
{
    public string Title { get; set; } = default!;

    public List<string> Ingredients { get; set; } = [];

    public string Description { get; set; } = default!;

    public List<string> Images { get; set; } = [];

    public string Author { get; set; } = default!;

    public void Update(
        string title,
        List<string> ingredients,
        string description,
        List<string> images,
        string author)
    {
        //TODO: guard clauses

        Title = title;
        Ingredients = ingredients;
        Description = description;
        Images = images;
        Author = author;
    }
}
