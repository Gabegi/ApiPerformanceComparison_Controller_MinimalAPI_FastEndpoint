using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks.Individual
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
    [BenchmarkCategory("FastEndpoints")]
    public class ProductsFastEndpointsBenchmark
    {
        private HttpClient? _client;
        private WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>? _factory;
        private readonly Random _random = new();

        private const int SMALL_DATASET = 1_000;
        private const int MEDIUM_DATASET = 10_000;
        private const int CONCURRENT_REQUESTS = 50;

        [GlobalSetup]
        public void Setup()
        {
            _factory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 100);
                        services.AddSingleton(testProducts);
                        
                        // CRITICAL: Register the IProductService properly for FastEndpoints
                        services.AddSingleton<FastEndpoints.FastEndpointsEntryPoint.Endpoints.IProductService, 
                                             ApiPerformanceComparison.FastEndpoints.Endpoints.InMemoryProductService>();
                    }));

            _client = _factory.CreateClient();
            
            // Add warmup to eliminate JIT compilation effects
            WarmupAsync().GetAwaiter().GetResult();
        }

        private async Task WarmupAsync()
        {
            // Warmup requests to stabilize performance
            for (int i = 0; i < 3; i++)
            {
                var response = await _client!.GetAsync("/products/1");
                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> GetSingleProduct()
        {
            // Use random ID to prevent caching effects
            var productId = _random.Next(1, 1000);
            var response = await _client!.GetAsync($"/products/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("SmallDataset")]
        public async Task<List<Product>?> GetSmallDataset()
        {
            var response = await _client!.GetAsync($"/products/list?count={SMALL_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        [BenchmarkCategory("MediumDataset")]
        public async Task<List<Product>?> GetMediumDataset()
        {
            var response = await _client!.GetAsync($"/products/list?count={MEDIUM_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        [BenchmarkCategory("CreateOperation")]
        public async Task CreateProduct()
        {
            var req = new CreateProductRequest { 
                Name = $"Product {_random.Next()}", // Unique name
                Price = (decimal)_random.NextDouble() * 100 
            };
            var response = await _client!.PostAsJsonAsync("/products", req);
            response.EnsureSuccessStatusCode();
            response.Dispose();
        }

        [Benchmark]
        [BenchmarkCategory("UpdateOperation")]
        public async Task UpdateProduct()
        {
            var productId = _random.Next(1, 100); // Random existing product
            var req = new UpdateProductRequest { 
                Name = $"Updated Product {_random.Next()}", 
                Price = (decimal)_random.NextDouble() * 100 
            };
            var response = await _client!.PutAsJsonAsync($"/products/{productId}", req);
            response.EnsureSuccessStatusCode();
            response.Dispose();
        }

        [Benchmark]
        [BenchmarkCategory("DeleteOperation")]
        public async Task DeleteProduct()
        {
            var productId = _random.Next(1, 100);
            var response = await _client!.DeleteAsync($"/products/{productId}");
            response.EnsureSuccessStatusCode();
            response.Dispose();
        }

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task ConcurrentSingleRequests()
        {
            var tasks = Enumerable.Range(0, CONCURRENT_REQUESTS)
                .Select(_ =>
                {
                    var productId = _random.Next(1, 1000);
                    return _client!.GetAsync($"/products/{productId}");
                });

            var responses = await Task.WhenAll(tasks);
            
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task ConcurrentSmallDatasets()
        {
            var tasks = Enumerable.Range(0, 10)
                .Select(_ => _client!.GetAsync($"/products/list?count={SMALL_DATASET}"));

            var responses = await Task.WhenAll(tasks);
            
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        // Separate cold start test
        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> ColdStartSingleRequest()
        {
            // Create fresh factory to measure true cold start
            using var factory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        var testProducts = QuickSeeder.SeedProducts(100);
                        services.AddSingleton(testProducts);
                        
                        // CRITICAL: Register the IProductService for cold start test too
                        services.AddSingleton<ApiPerformanceComparison.FastEndpoints.Endpoints.IProductService, 
                                             ApiPerformanceComparison.FastEndpoints.Endpoints.InMemoryProductService>();
                    }));
            using var client = factory.CreateClient();
            
            var response = await client.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }
    }
}