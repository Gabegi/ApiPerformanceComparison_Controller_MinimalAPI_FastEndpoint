using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.dotTrace;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
    [RankColumn]
    [AllStatisticsColumn]
    [OperationsPerSecond]
    [MinColumn, MaxColumn, MedianColumn]
    [DotTraceDiagnoser]
    [ThreadingDiagnoser]
    [JsonExporter]
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class ProductsApiBenchmark
    {
        private HttpClient? _controllerClient;
        private HttpClient? _minimalClient;
        private HttpClient? _fastEndpointsClient;

        private WebApplicationFactory<Controllers.ProductsController>? _controllerFactory;
        private WebApplicationFactory<MinimalApi.MinimalEntryPoint>? _minimalFactory;
        private WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>? _fastEndpointsFactory;

        // Optimal test data sizes
        private const int SMALL_DATASET = 1_000;      // Simulates typical page size
        private const int MEDIUM_DATASET = 10_000;    // Simulates moderate result set
        private const int CONCURRENT_REQUESTS = 50;   // Simulates moderate load

        [GlobalSetup]
        public void Setup()
        {
            // Setup Controller API
            _controllerFactory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                    builder
                        .UseEnvironment("Testing")
                        .UseSetting("environment", "Testing")
                        .ConfigureServices(services =>
                        {
                            // Seed with sufficient data for all tests
                            var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 100);
                            services.AddSingleton(testProducts);
                        })
                );
            _controllerClient = _controllerFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Setup Minimal API
            _minimalFactory = new WebApplicationFactory<MinimalApi.MinimalEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder
                        .UseEnvironment("Testing")
                        .ConfigureServices(services =>
                        {
                            var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 100);
                            services.AddSingleton(testProducts);
                        })
                );
            _minimalClient = _minimalFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Setup FastEndpoints API
            _fastEndpointsFactory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
                .WithWebHostBuilder(builder =>
                    builder
                        .UseEnvironment("Testing")
                        .ConfigureServices(services =>
                        {
                            var testProducts = QuickSeeder.SeedProducts(MEDIUM_DATASET + 100);
                            services.AddSingleton(testProducts);
                        })
                );
            _fastEndpointsClient = _fastEndpointsFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Warmup all endpoints to avoid JIT overhead in measurements
            WarmupEndpoints().GetAwaiter().GetResult();
        }

        private async Task WarmupEndpoints()
        {
            // Warmup each endpoint with a small request
            await _controllerClient!.GetAsync("/products/1");
            await _minimalClient!.GetAsync("/products/1");
            await _fastEndpointsClient!.GetAsync("/products/1");
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _controllerClient?.Dispose();
            _minimalClient?.Dispose();
            _fastEndpointsClient?.Dispose();
            _controllerFactory?.Dispose();
            _minimalFactory?.Dispose();
            _fastEndpointsFactory?.Dispose();
        }

        #region Single Request Latency Tests

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> Controller_GetSingleProduct()
        {
            var response = await _controllerClient!.GetAsync("/products/5");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> MinimalApi_GetSingleProduct()
        {
            var response = await _minimalClient!.GetAsync("/products/5");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("SingleRequest")]
        public async Task<Product?> FastEndpoints_GetSingleProduct()
        {
            var response = await _fastEndpointsClient!.GetAsync("/products/5");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        #endregion

        #region Small Dataset Tests (1k products)

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
            var response = await _minimalClient!.GetAsync($"/products/list?count={SMALL_DATASET}");
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

        #endregion

        #region Medium Dataset Tests (10k products)

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
            var response = await _minimalClient!.GetAsync($"/products/list?count={MEDIUM_DATASET}");
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

        #endregion

        #region Throughput Tests (Concurrent Requests)

        [Benchmark]
        [BenchmarkCategory("Throughput")]
        public async Task Controller_ConcurrentSingleRequests()
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 1; i <= CONCURRENT_REQUESTS; i++)
            {
                tasks.Add(_controllerClient!.GetAsync($"/products/{i}"));
            }

            var responses = await Task.WhenAll(tasks);

            // Ensure all requests succeeded
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
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 1; i <= CONCURRENT_REQUESTS; i++)
            {
                tasks.Add(_minimalClient!.GetAsync($"/products/{i}"));
            }

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
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 1; i <= CONCURRENT_REQUESTS; i++)
            {
                tasks.Add(_fastEndpointsClient!.GetAsync($"/products/{i}"));
            }

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
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 0; i < CONCURRENT_REQUESTS; i++)
            {
                tasks.Add(_controllerClient!.GetAsync($"/products/list?count=100"));
            }

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
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 0; i < CONCURRENT_REQUESTS; i++)
            {
                tasks.Add(_minimalClient!.GetAsync($"/products/list?count=100"));
            }

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
            var tasks = new List<Task<HttpResponseMessage>>();

            for (int i = 0; i < CONCURRENT_REQUESTS; i++)
            {
                tasks.Add(_fastEndpointsClient!.GetAsync($"/products/list?count=100"));
            }

            var responses = await Task.WhenAll(tasks);

            foreach (var response in responses)
            {
                response.EnsureSuccessStatusCode();
                response.Dispose();
            }
        }

        #endregion

        #region Cold Start Tests

        private HttpClient? _coldStartControllerClient;
        private HttpClient? _coldStartMinimalClient;
        private HttpClient? _coldStartFastEndpointsClient;

        [IterationSetup(Target = nameof(Controller_ColdStart))]
        public void SetupControllerColdStart()
        {
            _coldStartControllerClient?.Dispose();
            _coldStartControllerClient = _controllerFactory!.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [IterationSetup(Target = nameof(MinimalApi_ColdStart))]
        public void SetupMinimalApiColdStart()
        {
            _coldStartMinimalClient?.Dispose();
            _coldStartMinimalClient = _minimalFactory!.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [IterationSetup(Target = nameof(FastEndpoints_ColdStart))]
        public void SetupFastEndpointsColdStart()
        {
            _coldStartFastEndpointsClient?.Dispose();
            _coldStartFastEndpointsClient = _fastEndpointsFactory!.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> Controller_ColdStart()
        {
            var response = await _coldStartControllerClient!.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> MinimalApi_ColdStart()
        {
            var response = await _coldStartMinimalClient!.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [Benchmark]
        [BenchmarkCategory("ColdStart")]
        public async Task<Product?> FastEndpoints_ColdStart()
        {
            var response = await _coldStartFastEndpointsClient!.GetAsync("/products/1");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        [IterationCleanup(Target = nameof(Controller_ColdStart))]
        public void CleanupControllerColdStart()
        {
            _coldStartControllerClient?.Dispose();
        }

        [IterationCleanup(Target = nameof(MinimalApi_ColdStart))]
        public void CleanupMinimalApiColdStart()
        {
            _coldStartMinimalClient?.Dispose();
        }

        [IterationCleanup(Target = nameof(FastEndpoints_ColdStart))]
        public void CleanupFastEndpointsColdStart()
        {
            _coldStartFastEndpointsClient?.Dispose();
        }

        #endregion
    }
}