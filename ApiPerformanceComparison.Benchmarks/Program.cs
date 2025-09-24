using ApiPerformanceComparison.Benchmarks.BenchmarkDotnet;
using ApiPerformanceComparison.Benchmarks.LoadTesting;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<ProductsControllerBenchmark>();
BenchmarkRunner.Run<ProductsMinimalApiBenchmark>();
BenchmarkRunner.Run<ProductsFastEndpointsBenchmark>();
BenchmarkRunner.Run<ApiPerformanceComparisonBenchmark>();


//var loadTester = new ApiFrameworkLoadTests();
//loadTester.RunBasicCapacityTest();
//loadTester.RunMixedWorkloadTest();
//var endpoints = new[]
//                        {
//                            "http://localhost:5001",
//                            "http://localhost:5002",
//                            "http://localhost:5003"
//                        };

//foreach (var endpoint in endpoints)
//{
//    Console.WriteLine($"Testing {endpoint}...");
//    loadTester.RunBreakingPointTest(endpoint);
//}

