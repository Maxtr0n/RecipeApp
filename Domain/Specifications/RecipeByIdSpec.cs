using Ardalis.Specification;
using Domain.Entities;

namespace Domain.Specifications
{
    public class RecipeByIdSpec : SingleResultSpecification<Recipe>
    {
        public RecipeByIdSpec(Guid id)
        {
            Query.Where(r => r.Id == id);
        }
    }
}
