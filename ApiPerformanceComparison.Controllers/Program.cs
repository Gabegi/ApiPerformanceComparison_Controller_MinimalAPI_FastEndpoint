using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);
// Use Dictionary for O(1) lookups - same data structure for all frameworks
// Register services (actual seeding happens in benchmarks via WebApplicationFactory)
// builder.Services.AddSingleton<Dictionary<int, Product>>();
// builder.Services.AddSingleton<ConcurrentDictionary<int, Product>>();
// builder.Services.AddSingleton<AtomicCounter>();
// Correct registration for ProductsController (expects List<Product>)
builder.Services.AddSingleton(sp => QuickSeeder.SeedProducts(10_000).ToList());

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}


// ============================
// Seed products here for load testing 👇
// (commented out since controller uses List<Product> DI)
// ============================
// var products = app.Services.GetRequiredService<ConcurrentDictionary<int, Product>>();
// var counter = app.Services.GetRequiredService<AtomicCounter>();
// for (int i = 1; i <= 1000; i++)
// {
//     products[i] = new Product
//     {
//         Id = counter.GetNext(),
//         Name = $"Product {i}",
//         Price = i * 1.5m
//     };
// }
// ============================

app.MapControllers();
app.Run();

namespace ApiPerformanceComparison.Controllers
{
    public sealed class ControllerEntryPoint { }
}
