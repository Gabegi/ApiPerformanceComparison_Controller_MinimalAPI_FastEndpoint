using ApiPerformanceComparison.Shared;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Use Dictionary for O(1) lookups - same data structure as Controller
var productsDict = QuickSeeder.SeedProducts(10_000).ToDictionary(p => p.Id);
var maxId = new AtomicCounter(productsDict.Keys.Max());

builder.Services.AddSingleton(productsDict);
builder.Services.AddSingleton(maxId);

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}
app.MapGet("/products/list", (int count, [FromServices] Dictionary<int, Product> products) =>
    products.Values.Take(count).ToAsyncEnumerable());

app.MapGet("/products/{id:int}", (int id, [FromServices] Dictionary<int, Product> products) => 
    products.TryGetValue(id, out var p) ? Results.Ok(p) : Results.NotFound());

app.MapPost("/products", (Product newProduct, [FromServices] Dictionary<int, Product> products, [FromServices] AtomicCounter counter) =>
{
    newProduct.Id = counter.GetNext();
    products[newProduct.Id] = newProduct;
    return Results.Created($"/products/{newProduct.Id}", newProduct);
});

app.MapPut("/products/{id:int}", (int id, Product updatedProduct, [FromServices] Dictionary<int, Product> products) =>
{
    if (!products.TryGetValue(id, out var existing))
        return Results.NotFound();
    existing.Name = updatedProduct.Name;
    existing.Price = updatedProduct.Price;
    return Results.Ok(existing);
});

app.MapDelete("/products/{id:int}", (int id, [FromServices] Dictionary<int, Product> products) =>
{
    return products.Remove(id) ? Results.NoContent() : Results.NotFound();
});


app.Run();

namespace ApiPerformanceComparison.MinimalApi
{
    public sealed class MinimalEntryPoint { }
}