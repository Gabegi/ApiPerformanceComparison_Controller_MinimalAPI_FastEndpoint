using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.dotTrace;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks
{
    
    [MemoryDiagnoser] // memory allocation
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
    [RankColumn]
    [AllStatisticsColumn]
    [OperationsPerSecond]
    [MinColumn, MaxColumn, MedianColumn]
    [DotTraceDiagnoser] // CPU Usage
    [ThreadingDiagnoser] // lock contentions
    [JsonExporter]
    [GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    public class ProductsApiBenchmark
    {
        private HttpClient? _controllerClient;
        private HttpClient? _minimalClient;
        private WebApplicationFactory<Controllers.ProductsController>? _controllerFactory;
        private WebApplicationFactory<MinimalApi.MinimalEntryPoint>? _minimalFactory;
        private WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>? _fastEndpointsFactory;

        [GlobalSetup]
        public void Setup()
        {
            // Controller SetUp
            _controllerFactory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>

                builder
                    .UseEnvironment("Testing")
                    .UseSetting("environment", "Testing")
                );
            _controllerClient = _controllerFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Setup Minimal API
            _minimalFactory = new WebApplicationFactory<MinimalApi.MinimalEntryPoint>()
                .WithWebHostBuilder(builder =>
                {
                    builder
                        .UseEnvironment("Testing")
                        .ConfigureServices(services =>
                        {
                            var testProducts = QuickSeeder.SeedProducts(150_000);
                        });
                });

            _minimalClient = _minimalFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Setup FastEndpoints API
            _fastEndpointsFactory = new WebApplicationFactory<FastEndpoints.FastEndpointsEntryPoint>()
                .WithWebHostBuilder(builder =>
                {
                    builder
                        .UseEnvironment("Testing")
                        .ConfigureServices(services =>
                        {
                            var testProducts = QuickSeeder.SeedProducts(150_000);
                        });
                });

            _fastEndpointsClient = _fastEndpointsFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _controllerClient?.Dispose();
            _minimalClient?.Dispose();
            _controllerFactory?.Dispose();
            _minimalFactory?.Dispose();
            _fastEndpointsFactory?.Dispose();
        }


        // Controller API Benchmarks
        [Benchmark(Baseline = true)]
        public async Task<Product?> Controller_GetSingleProduct()
        {
            var product = await _controllerClient.GetFromJsonAsync<Product>("/products/5");
            return product;
        }

        [Benchmark]
        public async Task<List<Product>?> Controller_Get5kProducts()
        {
            var products = await _controllerClient.GetFromJsonAsync<List<Product>>("/products/list?count=5000");
            return products;
        }

        [Benchmark]
        public async Task<List<Product>?> Controller_Get50kProducts()
        {
            var products = await _controllerClient.GetFromJsonAsync<List<Product>>("/products/list?count=50000");
            return products;
        }

        [Benchmark]
        public async Task<List<Product>?> Controller_Get100kProducts()
        {
            var products = await _controllerClient.GetFromJsonAsync<List<Product>>("/products/list?count=100000");
            return products;
        }

        // Minimal API Benchmarks
        [Benchmark]
        public async Task<Product?> MinimalApi_GetSingleProduct()
        {
            var product = await _minimalClient.GetFromJsonAsync<Product>("/products/5");
            return product;
        }

        [Benchmark]
        public async Task<List<Product>?> MinimalApi_Get5kProducts()
        {
            var products = await _minimalClient.GetFromJsonAsync<List<Product>>("/products/list?count=5000");
            return products;
        }

        [Benchmark]
        public async Task<List<Product>?> MinimalApi_Get50kProducts()
        {
            var products = await _minimalClient.GetFromJsonAsync<List<Product>>("/products/list?count=50000");
            return products;
        }

        [Benchmark]
        public async Task<List<Product>?> MinimalApi_Get100kProducts()
        {
            var products = await _minimalClient.GetFromJsonAsync<List<Product>>("/products/list?count=100000");
            return products;
        }

        private HttpClient? _fastEndpointsClient;

        // FastEndpoints API Benchmarks
        [Benchmark]
        public async Task<Product?> FastEndpoints_GetSingleProduct()
        {
            var product = await _fastEndpointsClient.GetFromJsonAsync<Product>("/products/5");
            return product;
        }

        [Benchmark]
        public async Task<List<Product>?> FastEndpoints_Get5kProducts()
        {
            var products = await _fastEndpointsClient.GetFromJsonAsync<List<Product>>("/products/list?count=5000");
            return products;
        }

        [Benchmark]
        public async Task<List<Product>?> FastEndpoints_Get50kProducts()
        {
            var products = await _fastEndpointsClient.GetFromJsonAsync<List<Product>>("/products/list?count=50000");
            return products;
        }

        [Benchmark]
        public async Task<List<Product>?> FastEndpoints_Get100kProducts()
        {
            var products = await _fastEndpointsClient.GetFromJsonAsync<List<Product>>("/products/list?count=100000");
            return products;
        }
    }

}
