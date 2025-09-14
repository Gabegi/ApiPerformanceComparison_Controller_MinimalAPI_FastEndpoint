```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean | Error | Ratio | RatioSD | Alloc Ratio |
|--------------------------------------- |-------------- |-----:|------:|------:|--------:|------------:|
| Controller_ColdStart                   | ColdStart     |   NA |    NA |     ? |       ? |           ? |
| MinimalApi_ColdStart                   | ColdStart     |   NA |    NA |     ? |       ? |           ? |
| FastEndpoints_ColdStart                | ColdStart     |   NA |    NA |     ? |       ? |           ? |
|                                        |               |      |       |       |         |             |
| Controller_GetMediumDataset            | MediumDataset |   NA |    NA |     ? |       ? |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset |   NA |    NA |     ? |       ? |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset |   NA |    NA |     ? |       ? |           ? |
|                                        |               |      |       |       |         |             |
| Controller_GetSingleProduct            | SingleRequest |   NA |    NA |     ? |       ? |           ? |
| MinimalApi_GetSingleProduct            | SingleRequest |   NA |    NA |     ? |       ? |           ? |
| FastEndpoints_GetSingleProduct         | SingleRequest |   NA |    NA |     ? |       ? |           ? |
|                                        |               |      |       |       |         |             |
| Controller_GetSmallDataset             | SmallDataset  |   NA |    NA |     ? |       ? |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |   NA |    NA |     ? |       ? |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |   NA |    NA |     ? |       ? |           ? |
|                                        |               |      |       |       |         |             |
| Controller_ConcurrentSingleRequests    | Throughput    |   NA |    NA |     ? |       ? |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |   NA |    NA |     ? |       ? |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |   NA |    NA |     ? |       ? |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |   NA |    NA |     ? |       ? |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |   NA |    NA |     ? |       ? |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |   NA |    NA |     ? |       ? |           ? |

Benchmarks with issues:
  ApiPerformanceComparisonBenchmark.Controller_ColdStart: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.MinimalApi_ColdStart: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_ColdStart: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.Controller_GetMediumDataset: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.MinimalApi_GetMediumDataset: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_GetMediumDataset: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.Controller_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.MinimalApi_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.Controller_GetSmallDataset: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.MinimalApi_GetSmallDataset: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_GetSmallDataset: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.Controller_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.MinimalApi_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.Controller_ConcurrentSmallDatasets: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.MinimalApi_ConcurrentSmallDatasets: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_ConcurrentSmallDatasets: .NET 9.0(Runtime=.NET 9.0)
