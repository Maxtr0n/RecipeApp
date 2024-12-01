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
                        Array.Empty<string>()
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

        app.UseHttpsRedirection();

        app.MapIdentityApi<ApplicationUser>();

        app.UseExceptionHandler();

        app.MapControllers();

        return app;
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();

        // Check and apply pending migrations
        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        var migrations = pendingMigrations.ToList();
        if (migrations.Count != 0)
        {
            Console.WriteLine($"Applying {migrations.Count} migrations to  database...");
            dbContext.Database.Migrate();
            Console.WriteLine("Migrations applied successfully.");
        }
        else
        {
            Console.WriteLine("No pending migrations found.");
        }
    }
}