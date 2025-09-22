#nullable enable

using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register services (datasets will be injected by benchmarks)
builder.Services.AddSingleton<ConcurrentDictionary<int, Product>>();
builder.Services.AddSingleton<AtomicCounter>();

var app = builder.Build();

// Optional HTTPS redirection in non-testing environments
if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

// List products endpoint
app.MapGet("/products/list", (int count, [FromServices] ConcurrentDictionary<int, Product> products) =>
    products.Values.Take(count)
);

// Get single product by ID
app.MapGet("/products/{id:int}", (int id, [FromServices] ConcurrentDictionary<int, Product> products) =>
    products.TryGetValue(id, out var product) ? Results.Ok(product) : Results.NotFound()
);

// Create new product
app.MapPost("/products", (Product newProduct, [FromServices] ConcurrentDictionary<int, Product> products, [FromServices] AtomicCounter counter) =>
{
    if (newProduct is null)
        return Results.BadRequest();

    newProduct.Id = counter.GetNext();
    products[newProduct.Id] = newProduct;
    return Results.Created($"/products/{newProduct.Id}", newProduct);
});

// Update existing product
app.MapPut("/products/{id:int}", (int id, Product updatedProduct, [FromServices] ConcurrentDictionary<int, Product> products) =>
{
    if (!products.TryGetValue(id, out var existing))
        return Results.NotFound();

    existing.Name = updatedProduct.Name;
    existing.Price = updatedProduct.Price;
    return Results.Ok(existing);
});

// Delete product
app.MapDelete("/products/{id:int}", (int id, [FromServices] ConcurrentDictionary<int, Product> products) =>
    products.TryRemove(id, out _) ? Results.NoContent() : Results.NotFound()
);

app.Run();

namespace ApiPerformanceComparison.MinimalApi
{
    public sealed class MinimalEntryPoint { }
}
