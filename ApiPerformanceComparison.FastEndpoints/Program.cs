using FastEndpoints;
using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Use Dictionary for O(1) lookups - same data structure as others
var productsDict = QuickSeeder.SeedProducts(100_000).ToDictionary(p => p.Id);
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