using ApiPerformanceComparison.Shared;
using FastEndpoints;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints
{
    public class ProductsFastEndpointsBenchmark
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

            public override Task HandleAsync(CancellationToken ct)
            {
                var products = Resolve<List<Product>>();
                var count = Query<int?>("count");
                var take = count.GetValueOrDefault(50);

                return SendOkAsync(products);
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

            public override Task HandleAsync(CancellationToken ct)
            {
                var products = Resolve<List<Product>>();
                var id = Route<int>("id");
                var product = products.FirstOrDefault(p => p.Id == id);

                return product is null ? SendNotFoundAsync(ct) : SendOkAsync(product, ct);
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

            public override Task HandleAsync(Product newProduct, CancellationToken ct)
            {
                var products = Resolve<List<Product>>();

                if (newProduct == null)
                    return SendErrorsAsync(cancellation: ct);

                newProduct.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                products.Add(newProduct);

                return SendCreatedAtAsync<GetProductByIdEndpoint>(new { id = newProduct.Id }, newProduct);
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

            public override Task HandleAsync(Product updatedProduct, CancellationToken ct)
            {
                var id = Route<int>("id");
                var products = Resolve<List<Product>>();
                var existingProduct = products.FirstOrDefault(p => p.Id == id);

                if (existingProduct is null)
                    return SendNotFoundAsync(ct);

                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                return SendOkAsync(existingProduct, ct);
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

            public override Task HandleAsync(CancellationToken ct)
            {
                var id = Route<int>("id");
                var products = Resolve<List<Product>>();
                var product = products.FirstOrDefault(p => p.Id == id);

                if (product is null)
                    return SendNotFoundAsync(ct);

                products.Remove(product);
                return SendNoContentAsync(ct);
            }
        }
     }
}