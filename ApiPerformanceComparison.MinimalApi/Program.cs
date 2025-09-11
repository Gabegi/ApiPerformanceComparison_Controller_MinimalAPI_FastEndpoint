using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// // Data seeding once at startup
// var products = QuickSeeder.SeedProducts(100_000);
// builder.Services.AddSingleton(products);

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}
// GET /products/{id}
app.MapGet("/products/{id:int}", (int id, List<Product> products) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

// GET /products/list?count=50
app.MapGet("/products/list", (int? count, List<Product> products) =>
{
    var take = count.GetValueOrDefault(50);
    return Results.Ok(products.Take(take));
});

// POST /products
app.MapPost("/products", (Product newProduct, List<Product> products) =>
{
    if (newProduct == null)
        return Results.BadRequest();

    newProduct.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
    products.Add(newProduct);

    return Results.Created($"/products/{newProduct.Id}", newProduct);
});

// PUT /products/{id}
app.MapPut("/products/{id:int}", (int id, Product updatedProduct, List<Product> products) =>
{
    var existingProduct = products.FirstOrDefault(p => p.Id == id);
    if (existingProduct is null)
        return Results.NotFound();

    existingProduct.Name = updatedProduct.Name;
    existingProduct.Price = updatedProduct.Price;

    return Results.Ok(existingProduct);
});

// DELETE /products/{id}
app.MapDelete("/products/{id:int}", (int id, List<Product> products) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null)
        return Results.NotFound();

    products.Remove(product);
    return Results.NoContent();
});

app.Run();

//// This class is needed for WebApplicationFactory to work with minimal APIs
namespace ApiPerformanceComparison.MinimalApi
{
    public sealed class MinimalEntryPoint { }
}

//public partial class Program { }