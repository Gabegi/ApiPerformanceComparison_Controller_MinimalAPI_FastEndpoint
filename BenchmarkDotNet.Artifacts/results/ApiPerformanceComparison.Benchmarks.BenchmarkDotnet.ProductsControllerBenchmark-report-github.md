```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean        | Error       | StdDev      | Median      | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |------------:|------------:|------------:|------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 14,696.9 μs |   708.04 μs | 1,997.04 μs | 14,427.6 μs |  281.2500 |               7.0000 |                - |   93.7500 |         - |  1420.09 KB |
| CreateProduct            |    104.5 μs |     2.03 μs |     3.34 μs |    104.5 μs |    4.8828 |               2.0000 |                - |    0.4883 |         - |    20.59 KB |
| DeleteProduct            |          NA |          NA |          NA |          NA |        NA |                   NA |               NA |        NA |        NA |          NA |
| GetMediumDataset         | 24,688.4 μs |   921.21 μs | 2,490.56 μs | 23,756.3 μs | 1437.5000 |             136.6250 |                - | 1343.7500 | 1000.0000 | 10135.16 KB |
| GetSingleProduct         |    113.1 μs |    11.65 μs |    33.63 μs |    103.6 μs |    3.4180 |               2.0000 |                - |         - |         - |    14.98 KB |
| GetSmallDataset          |  5,308.4 μs |   153.71 μs |   438.56 μs |  5,268.7 μs |  140.6250 |              13.6719 |                - |  140.6250 |  140.6250 |   716.92 KB |
| ConcurrentSingleRequests |  1,540.7 μs |    52.00 μs |   145.82 μs |  1,495.7 μs |  144.5313 |             100.0273 |           0.0195 |   66.4063 |         - |   718.79 KB |
| ConcurrentSmallDatasets  | 10,451.8 μs | 1,001.51 μs | 2,873.51 μs | 10,296.0 μs |  906.2500 |             135.2188 |           9.1250 |  687.5000 |  593.7500 |   5876.5 KB |
| UpdateProduct            |    131.7 μs |     6.30 μs |    17.47 μs |    129.0 μs |    4.8828 |               2.0005 |           0.0005 |    0.4883 |         - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
