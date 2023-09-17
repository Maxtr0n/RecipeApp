using Application.Abstractions;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _context;

        public RecipeRepository(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task<Recipe> AddRecipe(Recipe toCreate)
        {
            _context.Recipes.Add(toCreate);
            await _context.SaveChangesAsync();

            return toCreate;
        }

        public Task DeleteRecipe(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Recipe>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeById(int recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> UpdateRecipe(int recipeId, string title, IEnumerable<string> ingredients, string description, IEnumerable<string> images, string author)
        {
            throw new NotImplementedException();
        }
    }
}
