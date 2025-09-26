```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 18,032.05 μs |   680.274 μs |  1,951.84 μs | 17,589.67 μs |  468.7500 |               9.0000 |                - |   62.5000 |         - |  2887.44 KB |
| CreateProduct            |     84.43 μs |     7.195 μs |     19.08 μs |     78.47 μs |    3.9063 |               2.0029 |                - |    0.4883 |         - |    16.59 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |
| GetMediumDataset         | 37,200.43 μs | 5,417.053 μs | 14,829.10 μs | 31,976.89 μs | 1444.4444 |             135.8889 |                - | 1333.3333 | 1000.0000 | 10347.06 KB |
| GetSingleProduct         |    132.49 μs |    25.497 μs |     75.18 μs |    122.41 μs |    2.9297 |               2.0012 |           0.0068 |         - |         - |     12.8 KB |
| GetSmallDataset          | 10,270.74 μs | 2,332.238 μs |  6,876.66 μs |  7,599.41 μs |  171.8750 |              15.9063 |                - |  125.0000 |   93.7500 |   819.15 KB |
| ConcurrentSingleRequests |  2,240.44 μs |   135.294 μs |    365.78 μs |  2,154.83 μs |  109.3750 |             100.1406 |           0.0313 |   46.8750 |         - |   606.65 KB |
| ConcurrentSmallDatasets  | 20,833.85 μs | 1,047.871 μs |  2,938.34 μs | 20,968.81 μs |  687.5000 |             141.1563 |           9.3750 |  593.7500 |  312.5000 |     5767 KB |
| UpdateProduct            |           NA |           NA |           NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsFastEndpointsBenchmark.UpdateProduct: .NET 9.0(Runtime=.NET 9.0)
