using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:17-alpine")
            .Build();

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RecipeDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<RecipeDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }
}