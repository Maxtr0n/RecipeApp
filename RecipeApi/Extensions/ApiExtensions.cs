using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

        services.AddIdentityApiEndpoints<ApplicationUser>(options =>
            {
            })
            .AddEntityFrameworkStores<RecipeDbContext>();

        services.AddHealthChecks();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        []
                    }
                });
            }
        );

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseSwagger();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.MapIdentityApi<ApplicationUser>();

        app.UseExceptionHandler();

        app.MapControllers();

        return app;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<RecipeDbContext>>();

        // Check and apply pending migrations
        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        var migrations = pendingMigrations.ToList();

        if (migrations.Count == 0)
        {
            logger.LogInformation("No pending migrations found.");
            Environment.Exit(0);
        }

        logger.LogInformation("Applying {MigrationsCount} migrations to  database...", migrations.Count);

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Applying database migrations failed.");
            Environment.Exit(1);
        }

        logger.LogInformation("Applying database migrations succeeded.");

        Environment.Exit(0);
    }
}