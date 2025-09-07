using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints;

public sealed class GetProductsListRequest
{
    public int? Count { get; set; }
}

public sealed class GetProductsListEndpoint : Endpoint<GetProductsListRequest, IEnumerable<Product>>
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
        var list = _products.Take(take);
        return SendOkAsync(list, ct);
    }
}


