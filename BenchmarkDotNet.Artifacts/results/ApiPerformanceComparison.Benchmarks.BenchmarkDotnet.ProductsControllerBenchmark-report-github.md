```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 12,955.76 μs |   357.495 μs |   941.784 μs | 12,784.43 μs |  187.5000 |   62.5000 |         - |  1219.98 KB |
| CreateProduct            |  1,149.52 μs |   156.791 μs |   462.300 μs |  1,136.83 μs |    4.8828 |         - |         - |    22.01 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 24,396.56 μs | 1,125.660 μs | 3,174.945 μs | 23,788.33 μs | 1466.6667 | 1333.3333 | 1000.0000 | 10147.16 KB |
| GetSingleProduct         |     79.31 μs |     1.225 μs |     2.177 μs |     78.67 μs |    3.4180 |         - |         - |     15.2 KB |
| GetSmallDataset          |  2,868.46 μs |   119.903 μs |   336.222 μs |  2,718.75 μs |  109.3750 |  109.3750 |  109.3750 |   744.77 KB |
| ConcurrentSingleRequests |    907.62 μs |    25.273 μs |    69.610 μs |    878.95 μs |  144.5313 |   74.2188 |         - |   724.29 KB |
| ConcurrentSmallDatasets  |  5,259.94 μs |   228.879 μs |   656.698 μs |  5,136.58 μs |  515.6250 |  468.7500 |  328.1250 |  5342.38 KB |
| UpdateProduct            |    106.21 μs |     2.760 μs |     7.649 μs |    103.37 μs |    4.8828 |         - |         - |    21.47 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
