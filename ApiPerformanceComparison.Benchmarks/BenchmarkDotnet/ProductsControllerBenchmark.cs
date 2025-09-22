using ApiPerformanceComparison.Shared;
using ApiPerformanceComparison.Shared.Requests;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks.BenchmarkDotnet
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
    [BenchmarkCategory("Controller")]
    public class ProductsControllerBenchmark
    {
        private HttpClient? _client;
        private WebApplicationFactory<Controllers.ProductsController>? _factory;
        private readonly Random _random = new();

        private const int SMALL_DATASET = 1_000;
        private const int MEDIUM_DATASET = 10_000;
        private const int CONCURRENT_REQUESTS = 50;

        [GlobalSetup]
        public void Setup()
        {
            var seeded = QuickSeeder.SeedProducts(100).ToDictionary(p => p.Id);
            var concurrentSeeded = new ConcurrentDictionary<int, Product>(seeded);

            _factory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 100).ToDictionary(p => p.Id);
                        services.AddSingleton(new ConcurrentDictionary<int, Product>(seeded));
                        services.AddSingleton(testProducts);
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
            var req = new CreateProductCall { 
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
            var req = new UpdateProductCall { 
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
            // 1. Create a unique product first
            var req = new CreateProductCall
            {
                Name = $"ToDelete {_random.Next()}",
                Price = (decimal)_random.NextDouble() * 100
            };

            var createResponse = await _client!.PostAsJsonAsync("/products", req);
            createResponse.EnsureSuccessStatusCode();
            var created = await createResponse.Content.ReadFromJsonAsync<Product>();
            createResponse.Dispose();

            // 2. Delete the newly created product
            var deleteResponse = await _client.DeleteAsync($"/products/{created!.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            deleteResponse.Dispose();
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
            var seeded = QuickSeeder.SeedProducts(100).ToDictionary(p => p.Id);
            var concurrentSeeded = new ConcurrentDictionary<int, Product>(seeded);

            // Create fresh factory to measure true cold start
            using var factory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(QuickSeeder.SeedProducts(100).ToDictionary(p => p.Id));
                                        services.AddSingleton(new ConcurrentDictionary<int, Product>(seeded));

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