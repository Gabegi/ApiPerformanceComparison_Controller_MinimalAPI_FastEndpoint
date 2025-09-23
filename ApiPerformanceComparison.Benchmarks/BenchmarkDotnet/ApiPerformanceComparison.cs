using ApiPerformanceComparison.Shared;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Net.Http.Json;

namespace ApiPerformanceComparison.Benchmarks.BenchmarkDotnet;

[MemoryDiagnoser]
[ThreadingDiagnoser] // ✅ capture contention & threads
[SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net90)]
[GroupBenchmarksBy(BenchmarkDotNet.Configs.BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class ApiPerformanceComparisonBenchmark
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

    // =============================================================================
    // FACTORY HELPER
    // =============================================================================
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

    // =============================================================================
    // GLOBAL SETUP
    // =============================================================================
    [GlobalSetup]
    public void Setup()
    {
        // Always seed at least MEDIUM_DATASET so queries never overflow
        _controllerFactory = CreateFactory<Controllers.ProductsController>(MEDIUM_DATASET + 100);
        _minimalApiFactory = CreateFactory<MinimalApi.MinimalEntryPoint>(MEDIUM_DATASET + 100);
        _fastEndpointsFactory = CreateFactory<FastEndpoints.FastEndpointsEntryPoint>(MEDIUM_DATASET + 100);

        _controllerClient = _controllerFactory.CreateClient();
        _minimalApiClient = _minimalApiFactory.CreateClient();
        _fastEndpointsClient = _fastEndpointsFactory.CreateClient();

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

        foreach (var task in warmupTasks)
            task.Result.Dispose();
    }

    // =============================================================================
    // COLD START TESTS
    // =============================================================================
    [Benchmark]
    [BenchmarkCategory("ColdStart")]
    public async Task<Product?> Controller_ColdStart()
    {
        using var factory = CreateFactory<Controllers.ProductsController>(100);
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/products/1");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    [Benchmark]
    [BenchmarkCategory("ColdStart")]
    public async Task<Product?> MinimalApi_ColdStart()
    {
        using var factory = CreateFactory<MinimalApi.MinimalEntryPoint>(100);
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/products/1");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    [Benchmark]
    [BenchmarkCategory("ColdStart")]
    public async Task<Product?> FastEndpoints_ColdStart()
    {
        using var factory = CreateFactory<FastEndpoints.FastEndpointsEntryPoint>(100);
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/products/1");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    // =============================================================================
    // SINGLE REQUEST TESTS
    // =============================================================================
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("SingleRequest")]
    public async Task<Product?> Controller_GetSingleProduct()
    {
        var productId = _random.Next(1, 1000);
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
        var tasks = Enumerable.Range(0, 10)
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

    // =============================================================================
    // CLEANUP
    // =============================================================================
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
