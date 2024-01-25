using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository<EntityBase>, GenericRepository<EntityBase>>();
            services.AddDbContext<RecipeDbContext>(options => options.UseSqlServer());
            return services;
        }
    }
}
