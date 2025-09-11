using ApiPerformanceComparison.Shared; // for Product, CreateProductRequest, UpdateProductRequest
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection; // for AddSingleton
using System.Net.Http.Json;



namespace ApiPerformanceComparison.Benchmarks
{
    [MemoryDiagnoser]
    [BenchmarkCategory("Controller")]
    public class ProductsControllerBenchmark
    {
        private HttpClient? _client;
        private WebApplicationFactory<Controllers.ProductsController>? _factory;

        private const int SMALL_DATASET = 1_000;
        private const int MEDIUM_DATASET = 10_000;
        private const int CONCURRENT_REQUESTS = 50;

        [GlobalSetup]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 100);
                        services.AddSingleton(testProducts);
                    }));

            _client = _factory.CreateClient();
        }

        [Benchmark]
        public async Task<Product?> GetSingleProduct()
        {
            var response = await _client!.GetAsync("/products/5");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        public async Task<List<Product>?> GetSmallDataset()
        {
            var response = await _client!.GetAsync($"/products/list?count={SMALL_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        public async Task<List<Product>?> GetMediumDataset()
        {
            var response = await _client!.GetAsync($"/products/list?count={MEDIUM_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        public async Task CreateProduct()
        {
            var req = new CreateProductRequest { Name = "New Product", Price = 99.99m };
            var response = await _client!.PostAsJsonAsync("/products", req);
            response.EnsureSuccessStatusCode();
        }

        [Benchmark]
        public async Task UpdateProduct()
        {
            var req = new UpdateProductRequest { Name = "Updated Product", Price = 88.88m };
            var response = await _client!.PutAsJsonAsync("/products/1", req);
            response.EnsureSuccessStatusCode();
        }

        [Benchmark]
        public async Task DeleteProduct()
        {
            var response = await _client!.DeleteAsync("/products/1");
            response.EnsureSuccessStatusCode();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }
    }
}
