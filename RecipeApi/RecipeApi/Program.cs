using Application;
using Infrastructure;
using RecipeApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .SetupApi(builder.Configuration);

var app = builder.Build();

if (args.Contains("migrate"))
{
    app.ApplyMigrations(true);
}
else if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

// Configure the HTTP request pipeline.
app.UseApi();

app.Run();

public partial class Program
{
}