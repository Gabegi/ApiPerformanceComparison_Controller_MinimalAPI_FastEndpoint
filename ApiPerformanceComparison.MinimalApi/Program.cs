using ApiPerformanceComparison.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Register product store once
builder.Services.AddSingleton(sp =>
{
    // seeded at startup (can be overridden in tests via ConfigureServices)
    var seeded = QuickSeeder.SeedProducts(10_000).ToDictionary(p => p.Id);
    return new ConcurrentDictionary<int, Product>(seeded);
});

var app = builder.Build();

// Get single product
app.MapGet("/products/{id:int}", ([FromRoute] int id, [FromServices] ConcurrentDictionary<int, Product> products) =>
    products.TryGetValue(id, out var product)
        ? Results.Ok(product)
        : Results.NotFound());

// List products (NO ToList → avoid allocations)
app.MapGet("/products/list", (int count, [FromServices] ConcurrentDictionary<int, Product> products) =>
    Results.Ok(products.Values.Take(count)));

// Create
app.MapPost("/products", (Product product, [FromServices] ConcurrentDictionary<int, Product> products) =>
{
    var id = products.Keys.DefaultIfEmpty(0).Max() + 1;
    product.Id = id;
    products.TryAdd(id, product);
    return Results.Created($"/products/{id}", product);
});

// Update
app.MapPut("/products/{id:int}", (int id, Product updated, [FromServices] ConcurrentDictionary<int, Product> products) =>
{
    if (!products.ContainsKey(id)) return Results.NotFound();
    updated.Id = id;
    products[id] = updated;
    return Results.Ok(updated);
});

// Delete
app.MapDelete("/products/{id:int}", (int id, [FromServices] ConcurrentDictionary<int, Product> products) =>
{
    return products.TryRemove(id, out _)
        ? Results.NoContent()
        : Results.NotFound();
});

app.Run();

namespace ApiPerformanceComparison.MinimalApi
{
    public partial class MinimalEntryPoint { }
}

