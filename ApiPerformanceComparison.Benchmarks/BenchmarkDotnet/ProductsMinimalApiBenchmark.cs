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
    [BenchmarkCategory("MinimalAPI")]
    public class ProductsMinimalApiBenchmark
    {
        private HttpClient? _client;
        private WebApplicationFactory<MinimalApi.MinimalEntryPoint>? _factory;
        private readonly Random _random = new();

        private const int SMALL_DATASET = 1_000;
        private const int MEDIUM_DATASET = 10_000;
        private const int CONCURRENT_REQUESTS = 50;

        private readonly List<int> _idsToUpdate = new();
        private readonly List<int> _idsToDelete = new();

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
        public async Task Setup()
        {
            _factory = CreateFactory<MinimalApi.MinimalEntryPoint>(MEDIUM_DATASET + 100);
            _client = _factory.CreateClient();

            await WarmupAsync();
        }

        private async Task WarmupAsync()
        {
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

        // ---------- Update ----------
        [GlobalSetup(Target = nameof(UpdateProduct))]
        public async Task SetupUpdateProducts()
        {
            _idsToUpdate.Clear();

            for (int i = 0; i < 1000; i++)
            {
                var req = new CreateProductCall
                {
                    Name = $"PreUpdate {_random.Next()}",
                    Price = (decimal)_random.NextDouble() * 100
                };

                var response = await _client!.PostAsJsonAsync("/products", req);
                response.EnsureSuccessStatusCode();

                var created = await response.Content.ReadFromJsonAsync<Product>();
                if (created != null) _idsToUpdate.Add(created.Id);

                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("UpdateOperation")]
        public async Task UpdateProduct()
        {
            var id = _idsToUpdate[_random.Next(_idsToUpdate.Count)];

            var req = new UpdateProductCall
            {
                Name = $"Updated Product {_random.Next()}",
                Price = (decimal)_random.NextDouble() * 100
            };

            var response = await _client!.PutAsJsonAsync($"/products/{id}", req);
            response.EnsureSuccessStatusCode();
            response.Dispose();
        }

        // ---------- Delete ----------
        [GlobalSetup(Target = nameof(DeleteProduct))]
        public async Task SetupDeleteProducts()
        {
            _idsToDelete.Clear();

            for (int i = 0; i < 1000; i++)
            {
                var req = new CreateProductCall
                {
                    Name = $"ToDelete {_random.Next()}",
                    Price = (decimal)_random.NextDouble() * 100
                };

                var response = await _client!.PostAsJsonAsync("/products", req);
                response.EnsureSuccessStatusCode();

                var created = await response.Content.ReadFromJsonAsync<Product>();
                if (created != null) _idsToDelete.Add(created.Id);

                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("DeleteOperation")]
        public async Task DeleteProduct()
        {
            if (_idsToDelete.Count == 0) return;

            var id = _idsToDelete[_random.Next(_idsToDelete.Count)];

            var response = await _client!.DeleteAsync($"/products/{id}");
            response.EnsureSuccessStatusCode();
            response.Dispose();
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
            using var factory = CreateFactory<MinimalApi.MinimalEntryPoint>(100);
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
