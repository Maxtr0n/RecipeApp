using Application;
using Infrastructure;
using RecipeApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.SetupApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApi();

app.Run();
