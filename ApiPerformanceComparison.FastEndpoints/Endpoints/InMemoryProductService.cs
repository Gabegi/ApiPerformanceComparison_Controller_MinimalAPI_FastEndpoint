
using FastEndpoints;
using ApiPerformanceComparison.Shared;
using ApiPerformanceComparison.FastEndpoints.Requests;
using Microsoft.AspNetCore.Authentication;
using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;
using FastEndpoints;
using ApiPerformanceComparison.Shared;


// entry point
namespace ApiPerformanceComparison.FastEndpoints
{
    public sealed class FastEndpointsEntryPoint { }
}

// GetProductsList Endpoint
public class GetProductsListEndpoint : Endpoint<GetProductsListRequest, IEnumerable<Product>>
{
    public override void Configure()
    {
        Get("/products/list");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetProductsListRequest req, CancellationToken ct)
{
    var products = Resolve<ConcurrentDictionary<int, Product>>();

    var result = products.Values.Take(req.Count);
    return SendOkAsync(result, ct);
}

}

// GetProduct Endpoint
public class GetProductEndpoint : Endpoint<GetProductRequest, Product>
{
    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {var products = Resolve<ConcurrentDictionary<int, Product>>();


        if (products.TryGetValue(req.Id, out var product))
            return SendOkAsync(product, ct);

        return SendNotFoundAsync(ct);
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

    public override Task HandleAsync(Product req, CancellationToken ct)
    {
        if (req == null)
        
            return SendErrorsAsync(cancellation: ct);

        var products = Resolve<ConcurrentDictionary<int, Product>>();

        var counter = Resolve<AtomicCounter>();

        req.Id = counter.GetNext();
        products[req.Id] = req;

        return SendCreatedAtAsync<GetProductEndpoint>(new { id = req.Id }, req, cancellation: ct);
    }
}

// UpdateProduct Endpoint
public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, Product>
{
    public override void Configure()
    {
        Put("/products/{id}");
        AllowAnonymous();
    }

    public override Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var products = Resolve<ConcurrentDictionary<int, Product>>();


        if (!products.TryGetValue(req.Id, out var existingProduct))
            return SendNotFoundAsync(ct);

        existingProduct.Name = req.Name;
        existingProduct.Price = req.Price;

        return SendOkAsync(existingProduct, ct);
    }
}

// DeleteProduct Endpoint
public class DeleteProductEndpoint : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var products = Resolve<ConcurrentDictionary<int, Product>>();


        if (products.TryRemove(req.Id, out _))
    return SendNoContentAsync(ct);

        return SendNotFoundAsync(ct);
    }
}
