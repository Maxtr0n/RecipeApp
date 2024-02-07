using Ardalis.Specification;
using Domain.Entities;

namespace Domain.Specifications;
public class RecipeByIdReadOnlySpec : SingleResultSpecification<Recipe>
{
    public RecipeByIdReadOnlySpec(Guid id)
    {
        Query.Where(r => r.Id == id)
            .AsNoTracking();
    }
}
