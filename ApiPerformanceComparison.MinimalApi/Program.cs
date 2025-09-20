using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Use Dictionary for O(1) lookups - same data structure as Controller
var productsDict = QuickSeeder.SeedProducts(100_000).ToDictionary(p => p.Id);
var maxId = new AtomicCounter(productsDict.Keys.Max());

builder.Services.AddSingleton(productsDict);
builder.Services.AddSingleton(maxId);

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

// All async without Task.FromResult wrapping
app.MapGet("/products/list", async (int? count, Dictionary<int, Product> products) => 
{
    var result = products.Values.Take(count).ToList();
    return Results.Ok(result);
});

app.MapGet("/products/{id:int}", async (int id, Dictionary<int, Product> products) => 
{
    if (products.TryGetValue(id, out var product))
        return Results.Ok(product);
    return Results.NotFound();
});

app.MapPost("/products", async (Product newProduct, Dictionary<int, Product> products, AtomicCounter counter) =>
{
    if (newProduct == null)
        return Results.BadRequest();

    newProduct.Id = counter.GetNext();
    products[newProduct.Id] = newProduct;
    return Results.Created($"/products/{newProduct.Id}", newProduct);
});

app.MapPut("/products/{id:int}", async (int id, Product updatedProduct, Dictionary<int, Product> products) =>
{
    if (!products.TryGetValue(id, out var existingProduct))
        return Results.NotFound();

    existingProduct.Name = updatedProduct.Name;
    existingProduct.Price = updatedProduct.Price;
    return Results.Ok(existingProduct);
});

app.MapDelete("/products/{id:int}", async (int id, Dictionary<int, Product> products) =>
{
    if (products.Remove(id))
        return Results.NoContent();
    return Results.NotFound();
});

app.Run();

namespace ApiPerformanceComparison.MinimalApi
{
    public sealed class MinimalEntryPoint { }
}