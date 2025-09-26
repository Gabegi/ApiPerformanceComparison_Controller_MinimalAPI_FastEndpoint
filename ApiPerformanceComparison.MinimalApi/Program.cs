using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Register product store and atomic counter
builder.Services.AddSingleton(sp =>
{
    var seeded = QuickSeeder.SeedProducts(10_000).ToDictionary(p => p.Id);
    return new ConcurrentDictionary<int, Product>(seeded);
});
builder.Services.AddSingleton(sp =>
{
    var products = sp.GetRequiredService<ConcurrentDictionary<int, Product>>();
    var maxId = products.Keys.DefaultIfEmpty(0).Max();
    return new AtomicCounter(maxId);
});

var app = builder.Build();

// Get single product
app.MapGet("/products/{id:int}", (int id, ConcurrentDictionary<int, Product> products) =>
    products.TryGetValue(id, out var product)
        ? Results.Ok(product)
        : Results.NotFound());

app.MapGet("/products/list", (int count, ConcurrentDictionary<int, Product> products) =>
{
    var result = products.Values.Take(count).ToList();
    return Results.Ok(result);
});



// Create
app.MapPost("/products", (Product product, ConcurrentDictionary<int, Product> products, AtomicCounter counter) =>
{
    var id = counter.GetNext();
    product.Id = id;
    products.TryAdd(id, product);
    return Results.Created($"/products/{id}", product);
});

// Update
app.MapPut("/products/{id:int}", (int id, Product updated, ConcurrentDictionary<int, Product> products) =>
{
    if (!products.ContainsKey(id)) return Results.NotFound();
    updated.Id = id;
    products[id] = updated;
    return Results.Ok(updated);
});

// Delete
app.MapDelete("/products/{id:int}", (int id, ConcurrentDictionary<int, Product> products) =>
    products.TryRemove(id, out _)
        ? Results.NoContent()
        : Results.NotFound());

app.Run();

namespace ApiPerformanceComparison.MinimalApi
{
    public partial class MinimalEntryPoint { }
}
