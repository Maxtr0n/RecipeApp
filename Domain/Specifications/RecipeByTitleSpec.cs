using Ardalis.Specification;
using Domain.Entities;

namespace Domain.Specifications;
public class RecipeByTitleSpec : Specification<Recipe>
{
    public RecipeByTitleSpec(string title)
    {
        Query.Where(r => r.Title == title);
    }
}
