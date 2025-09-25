using ApiPerformanceComparison.Benchmarks.BenchmarkDotnet;
using BenchmarkDotNet.Running;

internal class Program
{
    private static void Main(string[] args)
    {
        BenchmarkRunner.Run<ProductsControllerBenchmark>();
        BenchmarkRunner.Run<ProductsMinimalApiBenchmark>();
        BenchmarkRunner.Run<ProductsFastEndpointsBenchmark>();
        BenchmarkRunner.Run<ApiPerformanceComparisonBenchmark>();
    }
}




