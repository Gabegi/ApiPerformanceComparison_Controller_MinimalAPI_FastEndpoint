using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
// Use Dictionary for O(1) lookups - same data structure for all frameworks
// Register services (actual seeding happens in benchmarks via WebApplicationFactory)
builder.Services.AddSingleton<Dictionary<int, Product>>();
builder.Services.AddSingleton<ConcurrentDictionary<int, Product>>();
builder.Services.AddSingleton<AtomicCounter>();

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();

namespace ApiPerformanceComparison.Controllers
{
    public sealed class ControllerEntryPoint { }
}
