```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error      | StdDev     | Median       | Ratio | RatioSD | Gen0      | Gen1      | Gen2      | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-----------:|-----------:|-------------:|------:|--------:|----------:|----------:|----------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 11,351.20 μs | 225.070 μs | 508.021 μs | 11,092.87 μs |     ? |       ? |  296.8750 |  109.3750 |   15.6250 |  1404.43 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 11,771.40 μs | 234.665 μs | 336.550 μs | 11,740.48 μs |     ? |       ? |  250.0000 |  234.3750 |   15.6250 |   979.16 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 18,684.51 μs | 373.548 μs | 430.179 μs | 18,651.94 μs |     ? |       ? |  531.2500 |   31.2500 |         - |  2879.17 KB |           ? |
|                                        |               |              |            |            |              |       |         |           |           |           |             |             |
| Controller_GetMediumDataset            | MediumDataset |     77.29 μs |   1.533 μs |   2.248 μs |     77.72 μs |     ? |       ? |    3.6621 |         - |         - |     15.3 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 21,076.08 μs | 135.918 μs | 120.488 μs | 21,082.73 μs |     ? |       ? | 1468.7500 | 1437.5000 | 1000.0000 | 10344.01 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset |     33.13 μs |   1.185 μs |   3.399 μs |     33.78 μs |     ? |       ? |    3.1738 |         - |         - |    13.24 KB |           ? |
|                                        |               |              |            |            |              |       |         |           |           |           |             |             |
| Controller_GetSingleProduct            | SingleRequest |           NA |         NA |         NA |           NA |     ? |       ? |        NA |        NA |        NA |          NA |           ? |
| MinimalApi_GetSingleProduct            | SingleRequest |     23.30 μs |   0.459 μs |   0.917 μs |     23.15 μs |     ? |       ? |    2.6855 |         - |         - |    11.27 KB |           ? |
| FastEndpoints_GetSingleProduct         | SingleRequest |           NA |         NA |         NA |           NA |     ? |       ? |        NA |        NA |        NA |          NA |           ? |
|                                        |               |              |            |            |              |       |         |           |           |           |             |             |
| Controller_GetSmallDataset             | SmallDataset  |     74.59 μs |   1.487 μs |   3.038 μs |     74.77 μs |     ? |       ? |    3.6621 |         - |         - |    15.29 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  2,268.56 μs |  44.370 μs |  52.820 μs |  2,254.76 μs |     ? |       ? |  179.6875 |  109.3750 |   78.1250 |   811.33 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |     30.81 μs |   1.144 μs |   3.282 μs |     30.72 μs |     ? |       ? |    3.1738 |         - |         - |    13.22 KB |           ? |
|                                        |               |              |            |            |              |       |         |           |           |           |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |           NA |         NA |         NA |           NA |     ? |       ? |        NA |        NA |        NA |          NA |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    617.41 μs |  10.396 μs |  16.490 μs |    615.19 μs |     ? |       ? |  105.4688 |   50.7813 |         - |   531.24 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |           NA |         NA |         NA |           NA |     ? |       ? |        NA |        NA |        NA |          NA |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |    228.87 μs |   2.921 μs |   3.900 μs |    228.34 μs |     ? |       ? |   36.1328 |    5.8594 |         - |   148.63 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  5,104.11 μs | 106.430 μs | 301.924 μs |  5,024.68 μs |     ? |       ? |  812.5000 |  578.1250 |  421.8750 |  5794.68 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |    197.31 μs |   3.545 μs |   3.940 μs |    197.04 μs |     ? |       ? |   31.2500 |    3.9063 |         - |   128.64 KB |           ? |

Benchmarks with issues:
  ApiPerformanceComparisonBenchmark.Controller_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.Controller_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ApiPerformanceComparisonBenchmark.FastEndpoints_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
