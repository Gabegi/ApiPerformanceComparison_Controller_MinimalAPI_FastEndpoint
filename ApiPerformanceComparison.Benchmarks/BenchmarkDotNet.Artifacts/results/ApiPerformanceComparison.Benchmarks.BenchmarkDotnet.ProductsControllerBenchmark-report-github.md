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
| ColdStartSingleRequest   | 17,152.64 μs | 1,396.792 μs | 4,096.549 μs | 15,669.26 μs |  187.5000 |  156.2500 |         - |  1250.75 KB |
| CreateProduct            |    974.57 μs |   139.269 μs |   410.637 μs |    950.92 μs |    4.8828 |         - |         - |    22.01 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 22,448.42 μs |   647.887 μs | 1,869.301 μs | 21,924.86 μs | 1500.0000 | 1300.0000 | 1000.0000 | 10265.03 KB |
| GetSingleProduct         |     82.81 μs |     2.702 μs |     7.212 μs |     79.63 μs |    3.4180 |         - |         - |    15.19 KB |
| GetSmallDataset          |  3,332.42 μs |    97.260 μs |   275.910 μs |  3,200.50 μs |  109.3750 |  109.3750 |  109.3750 |   739.64 KB |
| ConcurrentSingleRequests |  1,131.53 μs |    22.423 μs |    61.005 μs |  1,122.96 μs |  144.5313 |   62.5000 |         - |    724.3 KB |
| ConcurrentSmallDatasets  |  5,739.91 μs |   143.699 μs |   414.605 μs |  5,702.94 μs |  546.8750 |  468.7500 |  343.7500 |  5355.64 KB |
| UpdateProduct            |    145.95 μs |     2.631 μs |     6.253 μs |    144.29 μs |    4.8828 |         - |         - |    21.47 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
