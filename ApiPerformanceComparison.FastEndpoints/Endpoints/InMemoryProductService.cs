using FastEndpoints;
using ApiPerformanceComparison.Shared;
using ApiPerformanceComparison.FastEndpoints.Requests;
using System.Collections.Concurrent;

//
// Shared AtomicCounter service for safe ID generation
//
public class AtomicCounter
{
    private int _counter;

    public AtomicCounter(int seed = 0)
    {
        _counter = seed;
    }

    public int GetNext() => Interlocked.Increment(ref _counter);
}

//
// GET /products/list
//
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

        var result = products.Values
                             .Take(req.Count)
                             .ToList();

        return SendOkAsync(result, ct);
    }
}

//
// GET /products/{id}
//
public class GetProductEndpoint : Endpoint<GetProductRequest, Product>
{
    private readonly ConcurrentDictionary<int, Product> _products;

    public GetProductEndpoint(ConcurrentDictionary<int, Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        if (_products.TryGetValue(req.Id, out var product))
            return SendOkAsync(product, ct);

        return SendNotFoundAsync(ct);
    }
}


//
// POST /products
//
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

//
// PUT /products/{id}
//
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

//
// DELETE /products/{id}
//
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
