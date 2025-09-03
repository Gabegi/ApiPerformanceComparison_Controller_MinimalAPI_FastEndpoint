using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks
{
    
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
    public class ProductsApiBenchmark
    {
        private HttpClient _client;
        private WebApplicationFactory<Controllers.ProductsController> _factory;

        [GlobalSetup]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>

                builder
                    .UseEnvironment("Testing")
                    .UseSetting("environment", "Testing")
                );
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Benchmark]
        public async Task GetSingleProduct()
        {
            var product = await _client.GetFromJsonAsync<Product>("/products/5");
        }

        [Benchmark]
        public async Task Get50kProducts()
        {
            var products = await _client.GetFromJsonAsync<List<Product>>("/products/list?count=50000");
        }
        [Benchmark]
        public async Task Get100kProducts()
        {
            var products = await _client.GetFromJsonAsync<List<Product>>("/products/list?count=100000");
        }
        [Benchmark]
        public async Task Get5kproducts()
        {
            var products = await _client.GetFromJsonAsync<List<Product>>("/products/list?count=5000");
        }
    }

}
