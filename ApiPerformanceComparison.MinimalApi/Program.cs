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

// Minimal API endpoints
app.MapGet("/products/{id:int}", (int id, List<Product> products) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product is null ? Results.NotFound() : Results.Ok(product);
});

app.MapGet("/products/list", (int? count, List<Product> products) =>
{
    var take = count.GetValueOrDefault(50);
    return Results.Ok(products.Take(take));
});

app.Run();

//namespace ApiPerformanceComparison.MinimalApi
//{
//    public sealed class MinimalEntryPoint { }
//}
