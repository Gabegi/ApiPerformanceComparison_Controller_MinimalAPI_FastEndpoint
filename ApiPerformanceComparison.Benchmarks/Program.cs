using ApiPerformanceComparison.Benchmarks;

BenchmarkRunner.Run<ProductsControllerBenchmark>();
BenchmarkRunner.Run<ProductsMinimalApiBenchmark>();
BenchmarkRunner.Run<ProductsFastEndpointsBenchmark>();
