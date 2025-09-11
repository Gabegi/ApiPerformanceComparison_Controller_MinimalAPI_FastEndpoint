using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints;

// Request DTOs
public sealed class GetProductRequest
{
    public int Id { get; set; }
}

public sealed class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public sealed class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

// GET /products/list
public class GetProductsListEndpoint : Endpoint<GetProductsListRequest, List<Product>>
{
    private readonly List<Product> _products;

    public GetProductsListEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Get("/products/list");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetProductsListRequest req, CancellationToken ct)
    {
        var take = req.Count.GetValueOrDefault(50);
        var result = _products.Take(take).ToList();
        return SendOkAsync(result, ct);
    }
}

// GET /products/{id}
public class GetProductByIdEndpoint : Endpoint<GetProductRequest, Product>
{
    private readonly List<Product> _products;

    public GetProductByIdEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Get("/products/{Id:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        var product = _products.FirstOrDefault(p => p.Id == req.Id);
        if (product is null)
            return SendNotFoundAsync(ct);

        return SendOkAsync(product, ct);
    }
}

// POST /products
public class CreateProductEndpoint : Endpoint<CreateProductRequest, Product>
{
    private readonly List<Product> _products;

    public CreateProductEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var newProduct = new Product
        {
            Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1,
            Name = req.Name,
            Price = req.Price
        };

        _products.Add(newProduct);
        return SendCreatedAtAsync<GetProductByIdEndpoint>(new { Id = newProduct.Id }, newProduct, ct);
    }
}

// PUT /products/{id}
public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, Product>
{
    private readonly List<Product> _products;

    public UpdateProductEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Put("/products/{id:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        if (!Route<int>("id", out var id))
            return SendBadRequestAsync(ct);

        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct is null)
            return SendNotFoundAsync(ct);

        existingProduct.Name = req.Name;
        existingProduct.Price = req.Price;

        return SendOkAsync(existingProduct, ct);
    }
}

// DELETE /products/{id}
public class DeleteProductEndpoint : EndpointWithoutRequest
{
    private readonly List<Product> _products;

    public DeleteProductEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Delete("/products/{id:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        if (!Route<int>("id", out var id))
            return SendBadRequestAsync(ct);

        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return SendNotFoundAsync(ct);

        _products.Remove(product);
        return SendNoContentAsync(ct);
    }
}
