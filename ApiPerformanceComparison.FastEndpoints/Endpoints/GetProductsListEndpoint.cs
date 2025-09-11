using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints;

public sealed class GetProductsListRequest
{
    public int? Count { get; set; }
}

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
