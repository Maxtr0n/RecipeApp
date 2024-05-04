using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using RecipeApi.Infrastructure;
using System.Net;

namespace RecipeApi.Extensions;

public static class ApiExtensions
{

    public static IServiceCollection SetupApi(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddControllers(mvcOptions => mvcOptions
            .AddResultConvention(resultStatusMap => resultStatusMap
                .AddDefaultMap()
                .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                    .For("POST", HttpStatusCode.Created)
                    .For("DELETE", HttpStatusCode.NoContent))
            ));

        services.AddAuthorization();
        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<RecipeDbContext>();

        services.AddHealthChecks();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapIdentityApi<IdentityUser>();

        app.UseExceptionHandler();

        app.MapControllers();

        app.MapHealthChecks("/health/database", new HealthCheckOptions
        {
            Predicate = healthCheck => healthCheck.Name.Equals("Database")
        });

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        return app;
    }
}
