using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Use Dictionary for O(1) lookups - same data structure for all frameworks
var productsDict = QuickSeeder.SeedProducts(10_000).ToDictionary(p => p.Id);
var maxId = new AtomicCounter(productsDict.Keys.Max());

builder.Services.AddSingleton(productsDict);
builder.Services.AddSingleton(maxId);
builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();