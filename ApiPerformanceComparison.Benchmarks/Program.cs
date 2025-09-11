using ApiPerformanceComparison.Benchmarks;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ProductsApiBenchmark>();

BenchmarkRunner.Run<ProductsControllerBenchmark>();
BenchmarkRunner.Run<ProductsMinimalApiBenchmark>();
BenchmarkRunner.Run<ProductsFastEndpointsBenchmark>();
