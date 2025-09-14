using ApiPerformanceComparison.Benchmarks.BenchmarkDotnet;
using ApiPerformanceComparison.FastEndpoints.Endpoints;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ProductsControllerBenchmark>();
BenchmarkRunner.Run<ProductsMinimalApiBenchmark>();
BenchmarkRunner.Run<ProductsFastEndpointsBenchmark>();
BenchmarkRunner.Run<ApiPerformanceComparisonBenchmark>();

