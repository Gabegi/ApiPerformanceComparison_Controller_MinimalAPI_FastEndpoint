using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints;

public class GetProductsListEndpoint : EndpointWithoutRequest<List<Product>>
{
    private readonly List<Product> _products;

    public GetProductsListEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(_products);
    }
}
