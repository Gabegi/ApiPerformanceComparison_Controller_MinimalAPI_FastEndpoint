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
    [ThreadingDiagnoser]
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

        // ================================================================
        // FACTORY HELPER
        // ================================================================
        private static WebApplicationFactory<T> CreateFactory<T>(int datasetSize) where T : class
        {
            var seeded = QuickSeeder.SeedProducts(datasetSize).ToDictionary(p => p.Id);
            var concurrentProducts = new ConcurrentDictionary<int, Product>(seeded);
            var counter = new AtomicCounter(seeded.Keys.Max());

            return new WebApplicationFactory<T>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(concurrentProducts);
                        services.AddSingleton(counter);
                    }));
        }

        // ================================================================
        // GLOBAL SETUP
        // ================================================================
        [GlobalSetup]
        public void Setup()
        {
            _factory = CreateFactory<Controllers.ProductsController>(MEDIUM_DATASET + 100);
            _client = _factory.CreateClient();

            WarmupAsync().GetAwaiter().GetResult();
        }

        private async Task WarmupAsync()
        {
            // Warmup a few requests to stabilize JIT/first-call effects
            for (int i = 0; i < 3; i++)
            {
                var response = await _client!.GetAsync("/products/1");
                response.Dispose();
            }
        }

        // ================================================================
        // SINGLE REQUEST TESTS
        // ================================================================
        [Benchmark]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> GetSingleProduct()
        {
            var productId = _random.Next(1, 1000);
            var response = await _client!.GetAsync($"/products/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        // ================================================================
        // DATASET TESTS
        // ================================================================
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

        // ================================================================
        // WRITE OPERATIONS
        // ================================================================
        [Benchmark]
        [BenchmarkCategory("CreateOperation")]
        public async Task CreateProduct()
        {
            var req = new CreateProductCall
            {
                Name = $"Product {_random.Next()}",
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
            var productId = _random.Next(1, 100); // valid seeded range
            var req = new UpdateProductCall
            {
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
            // Create product first
            var req = new CreateProductCall
            {
                Name = $"ToDelete {_random.Next()}",
                Price = (decimal)_random.NextDouble() * 100
            };

            var createResponse = await _client!.PostAsJsonAsync("/products", req);
            createResponse.EnsureSuccessStatusCode();
            var created = await createResponse.Content.ReadFromJsonAsync<Product>();
            createResponse.Dispose();

            // Then delete it
            var deleteResponse = await _client.DeleteAsync($"/products/{created!.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            deleteResponse.Dispose();
        }

        // ================================================================
        // CONCURRENCY / THROUGHPUT
        // ================================================================
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

        // ================================================================
        // COLD START
        // ================================================================
        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> ColdStartSingleRequest()
        {
            using var factory = CreateFactory<Controllers.ProductsController>(100);
            using var client = factory.CreateClient();

            var response = await client.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        // ================================================================
        // CLEANUP
        // ================================================================
        [GlobalCleanup]
        public void Cleanup()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }
    }
}
