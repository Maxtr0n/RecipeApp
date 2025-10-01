using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RecipeApi.Infrastructure;
using Scalar.AspNetCore;
using System.Net;

namespace RecipeApi.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection SetupApi(this IServiceCollection services, IConfiguration configuration)
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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                // In production set to true and use https when communicating with keycloak
                o.RequireHttpsMetadata = false;
                o.Authority = configuration["Auth:ValidIssuer"];
                o.Audience = configuration["Authentication:Audience"];
                o.MetadataAddress = configuration["Authentication:MetadataAddress"]!;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Authentication:ValidIssuer"],
                };
            });
        
        services.AddAuthorizationBuilder();
        
        AddOpenTelemetry(services);

        services.AddHealthChecks();

        services.AddOpenApi();

        return services;
    }

    private static void AddOpenTelemetry(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("RecipeApi"))
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation();
                
                tracing
                    .AddOtlpExporter();
            });
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapOpenApi();

        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
        }

        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseExceptionHandler();

        app.MapControllers();

        return app;
    }

    public static void ApplyMigrations(this WebApplication app, bool exit = false)
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
            if (exit)
            {
                Environment.Exit(0);
            }
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

        if (exit)
        {
            Environment.Exit(0);
        }
    }
}