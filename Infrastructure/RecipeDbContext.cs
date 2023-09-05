using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }

    }
}
