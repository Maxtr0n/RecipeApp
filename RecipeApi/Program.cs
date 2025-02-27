using Application;
using Infrastructure;
using RecipeApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .SetupApi();

var app = builder.Build();

//app.ApplyMigrations();

// Configure the HTTP request pipeline.
app.UseApi();

app.Run();

public partial class Program
{
}