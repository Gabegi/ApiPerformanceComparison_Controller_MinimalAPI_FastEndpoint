
using FastEndpoints;
using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Use Dictionary for O(1) lookups - same data structure as others
var productsDict = QuickSeeder.SeedProducts(100_000).ToDictionary(p => p.Id);
var maxId = new AtomicCounter(productsDict.Keys.Max());

builder.Services.AddSingleton(productsDict);
builder.Services.AddSingleton(maxId);
builder.Services.AddFastEndpoints();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.UseFastEndpoints();
app.Run();

// GetProductsList Endpoint
public class GetProductsListRequest
{
    public int? Count { get; set; } = 50;
}

public class GetProductsListEndpoint : Endpoint<GetProductsListRequest, List<Product>>
{
    private Dictionary<int, Product> _products = null!;

    public override void Configure()
    {
        Get("/products/list");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductsListRequest req, CancellationToken ct)
    {
        _products = Resolve<Dictionary<int, Product>>();
        var take = req.Count.GetValueOrDefault(50);
        var result = _products.Values.Take(take).ToList();
        await SendOkAsync(result, ct);
    }
}

// GetProduct Endpoint
public class GetProductRequest
{
    public int Id { get; set; }
}

public class GetProductEndpoint : Endpoint<GetProductRequest, Product>
{
    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        var products = Resolve<Dictionary<int, Product>>();
        
        if (products.TryGetValue(req.Id, out var product))
            await SendOkAsync(product, ct);
        else
            await SendNotFoundAsync(ct);
    }
}

// CreateProduct Endpoint
public class CreateProductEndpoint : Endpoint<Product, Product>
{
    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Product req, CancellationToken ct)
    {
        if (req == null)
        {
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var products = Resolve<Dictionary<int, Product>>();
        var counter = Resolve<AtomicCounter>();
        
        req.Id = counter.GetNext();
        products[req.Id] = req;
        
        await SendCreatedAtAsync<GetProductEndpoint>(new { id = req.Id }, req, cancellation: ct);
    }
}

// UpdateProduct Endpoint
public class UpdateProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, Product>
{
    public override void Configure()
    {
        Put("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var products = Resolve<Dictionary<int, Product>>();
        
        if (!products.TryGetValue(req.Id, out var existingProduct))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        existingProduct.Name = req.Name;
        existingProduct.Price = req.Price;
        
        await SendOkAsync(existingProduct, ct);
    }
}

// DeleteProduct Endpoint
public class DeleteProductRequest
{
    public int Id { get; set; }
}

public class DeleteProductEndpoint : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var products = Resolve<Dictionary<int, Product>>();
        
        if (products.Remove(req.Id))
            await SendNoContentAsync(ct);
        else
            await SendNotFoundAsync(ct);
    }
}

namespace ApiPerformanceComparison.FastEndpoints
{
    public sealed class FastEndpointsEntryPoint { }
}