using Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    public List<Recipe> Recipes { get; set; } = [];
}