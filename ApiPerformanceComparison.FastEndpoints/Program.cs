using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Seed products and use ConcurrentDictionary for thread-safe operations
var seeded = QuickSeeder.SeedProducts(10_000)
    .ToDictionary(p => p.Id);
var productsDict = new ConcurrentDictionary<int, Product>(seeded);

// Atomic counter to generate unique IDs
var maxId = new AtomicCounter(productsDict.Keys.Max());

// Register services
builder.Services.AddSingleton(productsDict);
builder.Services.AddSingleton(maxId);

// Register FastEndpoints
builder.Services.AddFastEndpoints();

var app = builder.Build();

// Enable HTTPS redirection if not in testing
if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

// Use FastEndpoints middleware
app.UseFastEndpoints();

// Run the app
app.Run();

// Optional: entry point marker for FastEndpoints
namespace ApiPerformanceComparison.FastEndpoints
{
    public sealed class FastEndpointsEntryPoint { }
}
