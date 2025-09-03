using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks
{

    [MemoryDiagnoser]
    public class ProductsApiBenchmark
    {
        private HttpClient _client;

        [GlobalSetup]
        public void Setup()
        {
            var appFactory = new WebApplicationFactory<Controllers.ProductsController>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("environment", "Testing");
                });
            _client = appFactory.CreateClient(new WebApplicationFactoryClientOptions
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
