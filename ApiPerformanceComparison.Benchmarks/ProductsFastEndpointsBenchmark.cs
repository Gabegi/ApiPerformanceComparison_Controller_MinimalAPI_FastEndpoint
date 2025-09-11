using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks
{
    [MemoryDiagnoser]
    [BenchmarkCategory("FastEndpoints")]
    public class ProductsFastEndpointsBenchmark
    {
        private HttpClient? _client;
        private WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>? _factory;

        private const int SMALL_DATASET = 1_000;
        private const int MEDIUM_DATASET = 10_000;

        [GlobalSetup]
        public void Setup()
        {
            _factory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
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
