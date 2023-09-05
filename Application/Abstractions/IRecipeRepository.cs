using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IRecipeRepository
    {
        Task<ICollection<Recipe>> GetAll();

        Task<Recipe> GetRecipeById(int recipeId);

        Task<Recipe> AddRecipe(Recipe toCreate);

        Task<Recipe> UpdateRecipe(int recipeId, string title, IEnumerable<string> ingredients,
            string description, IEnumerable<string> images, string author);

        Task DeleteRecipe(int recipeId);
    }
}
