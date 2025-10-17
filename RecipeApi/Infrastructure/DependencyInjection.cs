using Domain.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AuditableInterceptor>();
        services.AddDbContext<RecipeDbContext>((sp, options) =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .AddInterceptors(sp.GetRequiredService<AuditableInterceptor>()));

        services.AddHealthChecks()
            .AddDbContextCheck<RecipeDbContext>("Database");

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        return services;
    }
}