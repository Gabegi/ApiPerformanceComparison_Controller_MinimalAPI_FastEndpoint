using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints
{
    // ====================
    // Request DTOs
    // ====================
    public sealed class GetProductsListRequest
    {
        public int? Count { get; set; }
    }

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

    // ====================
    // Endpoints
    // ====================

    // GET /products/list
    public class GetProductsListEndpoint : Endpoint<GetProductsListRequest, List<Product>>
    {
        private readonly List<Product> _products;

        public GetProductsListEndpoint(List<Product> products) => _products = products;

        public override void Configure()
        {
            Get("/products/list");
            AllowAnonymous();
        }

        public override Task HandleAsync(GetProductsListRequest req, CancellationToken ct)
        {
            var take = req.Count.GetValueOrDefault(50);
            return SendOkAsync(_products.Take(take).ToList(), ct);
        }
    }

    // GET /products/{id}
    public class GetProductByIdEndpoint : Endpoint<GetProductRequest, Product>
    {
        private readonly List<Product> _products;

        public GetProductByIdEndpoint(List<Product> products) => _products = products;

        public override void Configure()
        {
            Get("/products/{id:int}");
            AllowAnonymous();
        }

        public override Task HandleAsync(GetProductRequest req, CancellationToken ct)
        {
            var product = _products.FirstOrDefault(p => p.Id == req.Id);
            return product is null ? SendNotFoundAsync(ct) : SendOkAsync(product, ct);
        }
    }

    // POST /products
    public class CreateProductEndpoint : Endpoint<CreateProductRequest, Product>
    {
        private readonly List<Product> _products;

        public CreateProductEndpoint(List<Product> products) => _products = products;

        public override void Configure()
        {
            Post("/products");
            AllowAnonymous();
        }

        public override Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var nextId = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
        var newProduct = new Product
        {
            Id = nextId,
            Name = req.Name,
            Price = req.Price
        };

        _products.Add(newProduct);

        // Correct usage in v5+: no CancellationToken parameter
        return SendCreatedAtAsync<GetProductByIdEndpoint>(new { id = newProduct.Id }, newProduct);
    }
    }

    // PUT /products/{id}
    public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, Product>
{
    private readonly List<Product> _products;
    public UpdateProductEndpoint(List<Product> products) => _products = products;

    public override void Configure()
    {
        Put("/products/{id:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var id = Route<int>("id"); // <- correct way to get route parameter

        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return SendNotFoundAsync(ct);

        product.Name = req.Name;
        product.Price = req.Price;

        return SendOkAsync(product, ct);
    }
}


    // DELETE /products/{id}
    public class DeleteProductEndpoint : EndpointWithoutRequest
{
    private readonly List<Product> _products;

    public DeleteProductEndpoint(List<Product> products) => _products = products;

    public override void Configure()
    {
        Delete("/products/{id:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        // Get the route parameter safely
        var id = Route<int>("id");

        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return SendNotFoundAsync(ct);

        _products.Remove(product);
        return SendNoContentAsync(ct);
    }
}

}
