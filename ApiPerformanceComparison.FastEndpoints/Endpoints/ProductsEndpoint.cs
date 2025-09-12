using ApiPerformanceComparison.Shared;
using FastEndpoints;
using ApiPerformanceComparison.FastEndpoints.Interfaces;

namespace ApiPerformanceComparison.FastEndpoints.Endpoints
{

    public class InMemoryProductService : IProductService
    {
        private readonly List<Product> _products;

        public InMemoryProductService(List<Product> products)
        {
            _products = products;
        }

        public List<Product> GetProducts() => _products;

        public List<Product> GetProducts(int count) => _products.Take(count).ToList();

        public Product? GetProduct(int id) => _products.FirstOrDefault(p => p.Id == id);

        public Product CreateProduct(string name, decimal price)
        {
            var nextId = _products.Count == 0 ? 1 : _products.Max(p => p.Id) + 1;
            var newProduct = new Product
            {
                Id = nextId,
                Name = name,
                Price = price
            };

            _products.Add(newProduct);
            return newProduct;
        }

        public Product? UpdateProduct(int id, string name, decimal price)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return null;

            product.Name = name;
            product.Price = price;
            return product;
        }

        public bool DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return false;

            _products.Remove(product);
            return true;
        }
    }

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
    // Endpoints (Performance Optimized)
    // ====================

    // GET /products/list
    public class GetProductsListEndpoint : Endpoint<GetProductsListRequest, List<Product>>
    {
        public override void Configure()
        {
            Get("/products/list");
            AllowAnonymous();
        }

        public override Task HandleAsync(GetProductsListRequest req, CancellationToken ct)
        {
            var productService = Resolve<IProductService>();
            var take = req.Count.GetValueOrDefault(50);
            var products = productService.GetProducts(take);
            
            return SendOkAsync(products, ct);
        }
    }

    // GET /products/{id}
    public class GetProductByIdEndpoint : Endpoint<GetProductRequest, Product>
    {
        public override void Configure()
        {
            Get("/products/{id:int}");
            AllowAnonymous();
        }

        public override Task HandleAsync(GetProductRequest req, CancellationToken ct)
        {
            var productService = Resolve<IProductService>();
            var product = productService.GetProduct(req.Id);
            
            return product is null ? SendNotFoundAsync(ct) : SendOkAsync(product, ct);
        }
    }

    // POST /products
    public class CreateProductEndpoint : Endpoint<CreateProductRequest, Product>
    {
        public override void Configure()
        {
            Post("/products");
            AllowAnonymous();
        }

        public override Task HandleAsync(CreateProductRequest req, CancellationToken ct)
        {
            var productService = Resolve<IProductService>();
            var newProduct = productService.CreateProduct(req.Name, req.Price);

            return SendCreatedAtAsync<GetProductByIdEndpoint>(new { id = newProduct.Id }, newProduct);
        }
    }

    // PUT /products/{id}
    public class UpdateProductEndpoint : Endpoint<UpdateProductRequest, Product>
    {
        public override void Configure()
        {
            Put("/products/{id:int}");
            AllowAnonymous();
        }

        public override Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        {
            var id = Route<int>("id");
            var productService = Resolve<IProductService>();
            var updatedProduct = productService.UpdateProduct(id, req.Name, req.Price);

            return updatedProduct is null ? SendNotFoundAsync(ct) : SendOkAsync(updatedProduct, ct);
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
            var productService = Resolve<IProductService>();
            var deleted = productService.DeleteProduct(id);

            return deleted ? SendNoContentAsync(ct) : SendNotFoundAsync(ct);
        }
    }
}