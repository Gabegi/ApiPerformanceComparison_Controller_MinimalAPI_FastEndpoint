using ApiPerformanceComparison.Shared;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// seed and DI
var products = QuickSeeder.SeedProducts(100_000);
builder.Services.AddSingleton(products);

builder.Services.AddFastEndpoints();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.UseFastEndpoints();

app.Run();

// This class is needed for WebApplicationFactory to work with FastEndpoints app
namespace ApiPerformanceComparison.FastEndpoints
{
    public sealed class FastEndpointsEntryPoint { }
}