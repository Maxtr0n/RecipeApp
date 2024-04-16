using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {

    }

    public List<Recipe> Recipes { get; set; } = [];
}
