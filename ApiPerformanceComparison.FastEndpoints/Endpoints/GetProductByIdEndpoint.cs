using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints;

public sealed class GetProductByIdRequest
{
    public int Id { get; set; }
}

public sealed class GetProductByIdEndpoint : Endpoint<GetProductByIdRequest, Product?>
{
    private readonly List<Product> _products;

    public GetProductByIdEndpoint(List<Product> products)
    {
        _products = products;
    }

    public override void Configure()
    {
        Get("/products/{id:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(GetProductByIdRequest req, CancellationToken ct)
    {
        var product = _products.FirstOrDefault(p => p.Id == req.Id);
        if (product is null)
        {
            return SendNotFoundAsync(ct);
        }

        return SendOkAsync(product, ct);
    }
}