using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints
{
    // ====================
    // Endpoints (No DTOs - Maximum Fair Comparison)
    // ====================

    // GET /products/list
    public class GetProductsListEndpoint : EndpointWithoutRequest<List<Product>>
    {
        public override void Configure()
        {
            Get("/products/list");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var products = Resolve<List<Product>>();
            var count = Query<int?>("count");
            var take = count.GetValueOrDefault(50);

            await SendOkAsync(products.Take(take).ToList(), ct);
        }
    }

    // GET /products/{id}
    public class GetProductByIdEndpoint : EndpointWithoutRequest<Product>
    {
        public override void Configure()
        {
            Get("/products/{id:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var products = Resolve<List<Product>>();
            var id = Route<int>("id");
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product is null)
                await SendNotFoundAsync(ct);
            else
                await SendOkAsync(product, ct);
        }
    }

    // POST /products
    public class CreateProductEndpoint : Endpoint<Product, Product>
    {
        public override void Configure()
        {
            Post("/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Product newProduct, CancellationToken ct)
        {
            var products = Resolve<List<Product>>();

            if (newProduct == null)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            newProduct.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(newProduct);

            await SendCreatedAtAsync<GetProductByIdEndpoint>(new { id = newProduct.Id }, newProduct, cancellation: ct);
        }
    }

    // PUT /products/{id}
    public class UpdateProductEndpoint : Endpoint<Product, Product>
    {
        public override void Configure()
        {
            Put("/products/{id:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Product updatedProduct, CancellationToken ct)
        {
            var id = Route<int>("id");
            var products = Resolve<List<Product>>();
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct is null)
                await SendNotFoundAsync(ct);
            else
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                await SendOkAsync(existingProduct, ct);
            }
        }
    }

    // DELETE /products/{id}
    public class DeleteProductEndpoint : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Delete("/products/{id:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var products = Resolve<List<Product>>();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product is null)
                await SendNotFoundAsync(ct);
            else
            {
                products.Remove(product);
                await SendNoContentAsync(ct);
            }
        }
    }
}