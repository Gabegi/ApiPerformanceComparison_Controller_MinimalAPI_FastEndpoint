using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints;

public sealed class GetProductsListRequest
{
    public int? Count { get; set; }
}

public sealed class GetProductsListEndpoint : EndpointWithoutRequest<List<Product>>
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

    public override async Task HandleAsync(CancellationToken ct)
    {
       return await SendAsync(products);
    }
}


