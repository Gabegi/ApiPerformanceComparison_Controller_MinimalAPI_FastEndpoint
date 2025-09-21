using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;
using FastEndpoints;
using ApiPerformanceComparison.Shared;


var builder = WebApplication.CreateBuilder(args);

// Use Dictionary for O(1) lookups - same data structure as others
var seeded = QuickSeeder.SeedProducts(10_000)
    .ToDictionary(p => p.Id);
var productsDict = new ConcurrentDictionary<int, Product>(seeded);
var maxId = new AtomicCounter(productsDict.Keys.Max());

builder.Services.AddSingleton(productsDict);
builder.Services.AddSingleton(maxId);
builder.Services.AddFastEndpoints();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.UseFastEndpoints();
app.Run();