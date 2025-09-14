using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Data seeding once at startup
var products = QuickSeeder.SeedProducts(100_000);
builder.Services.AddSingleton(products);

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.MapGet("/products/list", async (int? count, List<Product> products) => {
    var take = count.GetValueOrDefault(50);
    return await Task.FromResult(products.Take(take).ToList());
});

app.MapGet("/products/{id:int}", async (int id, List<Product> products) => {
    var product = products.FirstOrDefault(p => p.Id == id);
    return await Task.FromResult(product);
});

app.MapPost("/products", async (Product newProduct, List<Product> products) =>
{
    if (newProduct == null)
        return Results.BadRequest();

    newProduct.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
    products.Add(newProduct);

    return await Task.FromResult(Results.Created($"/products/{newProduct.Id}", newProduct));
});

app.MapPut("/products/{id:int}", async (int id, Product updatedProduct, List<Product> products) =>
{
    var existingProduct = products.FirstOrDefault(p => p.Id == id);
    if (existingProduct is null)
        return Results.NotFound();

    existingProduct.Name = updatedProduct.Name;
    existingProduct.Price = updatedProduct.Price;

    return await Task.FromResult(Results.Ok(existingProduct));
});

app.MapDelete("/products/{id:int}", async (int id, List<Product> products) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null)
        return Results.NotFound();

    products.Remove(product);
    return await Task.FromResult(Results.NoContent());
});

app.Run();

namespace ApiPerformanceComparison.MinimalApi
{
    public sealed class MinimalEntryPoint { }
}