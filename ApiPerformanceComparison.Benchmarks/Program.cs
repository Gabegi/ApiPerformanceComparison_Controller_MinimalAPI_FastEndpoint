using ApiPerformanceComparison.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ProductsControllerBenchmark>();
BenchmarkRunner.Run<ProductsMinimalApiBenchmark>();
BenchmarkRunner.Run<ProductsFastEndpointsBenchmark>();
