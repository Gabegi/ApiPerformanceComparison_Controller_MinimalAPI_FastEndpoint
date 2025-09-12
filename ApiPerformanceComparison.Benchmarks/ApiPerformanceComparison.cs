using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks;

[MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class ProductsApiBenchmark
    {
        private HttpClient? _controllerClient;
        private HttpClient? _minimalApiClient;
        private HttpClient? _fastEndpointsClient;
        
        private WebApplicationFactory<Controllers.ProductsController>? _controllerFactory;
        private WebApplicationFactory<MinimalApi.MinimalEntryPoint>? _minimalApiFactory;
        private WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>? _fastEndpointsFactory;

        private const int SMALL_DATASET = 1_000;
        private const int MEDIUM_DATASET = 10_000;
        private const int CONCURRENT_REQUESTS = 50;
        private readonly Random _random = new();

        [GlobalSetup]
        public void Setup()
        {
            var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 1000);

            // Setup Controller API
            _controllerFactory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(testProducts);
                    }));
            _controllerClient = _controllerFactory.CreateClient();

            // Setup Minimal API
            _minimalApiFactory = new WebApplicationFactory<MinimalApi.MinimalEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(testProducts);
                    }));
            _minimalApiClient = _minimalApiFactory.CreateClient();

            // Setup FastEndpoints API
            _fastEndpointsFactory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(testProducts);
                    }));
            _fastEndpointsClient = _fastEndpointsFactory.CreateClient();

            // Warmup requests to eliminate cold start effects
            WarmupAsync().GetAwaiter().GetResult();
        }

        private async Task WarmupAsync()
        {
            var warmupTasks = new[]
            {
                _controllerClient!.GetAsync("/products/1"),
                _minimalApiClient!.GetAsync("/products/1"),
                _fastEndpointsClient!.GetAsync("/products/1")
            };

            await Task.WhenAll(warmupTasks);
            
            // Dispose warmup responses
            foreach (var task in warmupTasks)
            {
                task.Result.Dispose();
            }
        }

        // =============================================================================
        // COLD START TESTS (measure first request after setup)
        // =============================================================================

        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> Controller_ColdStart()
        {
            // Create fresh factory to measure true cold start
            using var factory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(QuickSeeder.SeedProducts(100));
                    }));
            using var client = factory.CreateClient();
            
            var response = await client.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> MinimalApi_ColdStart()
        {
            using var factory = new WebApplicationFactory<MinimalApi.MinimalEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(QuickSeeder.SeedProducts(100));
                    }));
            using var client = factory.CreateClient();
            
            var response = await client.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> FastEndpoints_ColdStart()
        {
            using var factory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(QuickSeeder.SeedProducts(100));
                    }));
            using var client = factory.CreateClient();
            
            var response = await client.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        // =============================================================================
        // SINGLE REQUEST TESTS (warmed up)
        // =============================================================================

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> Controller_GetSingleProduct()
        {
            var productId = _random.Next(1, 1000); // Random product to avoid caching
            var response = await _controllerClient!.GetAsync($"/products/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> MinimalApi_GetSingleProduct()
        {
            var productId = _random.Next(1, 1000);
            var response = await _minimalApiClient!.GetAsync($"/products/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> FastEndpoints_GetSingleProduct()
        {
            var productId = _random.Next(1, 1000);
            var response = await _fastEndpointsClient!.GetAsync($"/products/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        // =============================================================================
        // SMALL DATASET TESTS
        // =============================================================================

        [Benchmark]
        [BenchmarkCategory("SmallDataset")]
        public async Task<List<Product>?> Controller_GetSmallDataset()
        {
            var response = await _controllerClient!.GetAsync($"/products/list?count={SMALL_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        [BenchmarkCategory("SmallDataset")]
        public async Task<List<Product>?> MinimalApi_GetSmallDataset()
        {
            var response = await _minimalApiClient!.GetAsync($"/products/list?count={SMALL_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        [BenchmarkCategory("SmallDataset")]
        public async Task<List<Product>?> FastEndpoints_GetSmallDataset()
        {
            var response = await _fastEndpointsClient!.GetAsync($"/products/list?count={SMALL_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        // =============================================================================
        // MEDIUM DATASET TESTS
        // =============================================================================

        [Benchmark]
        [BenchmarkCategory("MediumDataset")]
        public async Task<List<Product>?> Controller_GetMediumDataset()
        {
            var response = await _controllerClient!.GetAsync($"/products/list?count={MEDIUM_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        [BenchmarkCategory("MediumDataset")]
        public async Task<List<Product>?> MinimalApi_GetMediumDataset()
        {
            var response = await _minimalApiClient!.GetAsync($"/products/list?count={MEDIUM_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        [Benchmark]
        [BenchmarkCategory("MediumDataset")]
        public async Task<List<Product>?> FastEndpoints_GetMediumDataset()
        {
            var response = await _fastEndpointsClient!.GetAsync($"/products/list?count={MEDIUM_DATASET}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        // =============================================================================
        // CONCURRENT/THROUGHPUT TESTS
        // =============================================================================

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task Controller_ConcurrentSingleRequests()
        {
            var tasks = Enumerable.Range(0, CONCURRENT_REQUESTS)
                .Select(_ =>
                {
                    var productId = _random.Next(1, 1000);
                    return _controllerClient!.GetAsync($"/products/{productId}");
                });

            var responses = await Task.WhenAll(tasks);
            
            // Dispose all responses
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task MinimalApi_ConcurrentSingleRequests()
        {
            var tasks = Enumerable.Range(0, CONCURRENT_REQUESTS)
                .Select(_ =>
                {
                    var productId = _random.Next(1, 1000);
                    return _minimalApiClient!.GetAsync($"/products/{productId}");
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
        public async Task FastEndpoints_ConcurrentSingleRequests()
        {
            var tasks = Enumerable.Range(0, CONCURRENT_REQUESTS)
                .Select(_ =>
                {
                    var productId = _random.Next(1, 1000);
                    return _fastEndpointsClient!.GetAsync($"/products/{productId}");
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
        public async Task Controller_ConcurrentSmallDatasets()
        {
            var tasks = Enumerable.Range(0, 10) // Fewer concurrent requests for larger payloads
                .Select(_ => _controllerClient!.GetAsync($"/products/list?count={SMALL_DATASET}"));

            var responses = await Task.WhenAll(tasks);
            
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task MinimalApi_ConcurrentSmallDatasets()
        {
            var tasks = Enumerable.Range(0, 10)
                .Select(_ => _minimalApiClient!.GetAsync($"/products/list?count={SMALL_DATASET}"));

            var responses = await Task.WhenAll(tasks);
            
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task FastEndpoints_ConcurrentSmallDatasets()
        {
            var tasks = Enumerable.Range(0, 10)
                .Select(_ => _fastEndpointsClient!.GetAsync($"/products/list?count={SMALL_DATASET}"));

            var responses = await Task.WhenAll(tasks);
            
            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _controllerClient?.Dispose();
            _minimalApiClient?.Dispose();
            _fastEndpointsClient?.Dispose();
            
            _controllerFactory?.Dispose();
            _minimalApiFactory?.Dispose();
            _fastEndpointsFactory?.Dispose();
        }
    }
