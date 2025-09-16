```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean        | Error        | StdDev       | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |------------:|-------------:|-------------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 99,213.8 μs | 14,570.30 μs | 42,271.09 μs | 108,820.4 μs |  200.0000 |         - |         - |  1226.04 KB |
| CreateProduct            |  1,554.3 μs |    267.00 μs |    761.77 μs |   1,608.6 μs |    5.3711 |    0.9766 |         - |    22.01 KB |
| DeleteProduct            |          NA |           NA |           NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 26,554.5 μs |  1,966.86 μs |  5,515.30 μs |  25,885.9 μs | 1444.4444 | 1333.3333 | 1000.0000 | 10261.96 KB |
| GetSingleProduct         |    226.9 μs |     27.83 μs |     75.70 μs |     256.6 μs |    3.6621 |         - |         - |     15.2 KB |
| GetSmallDataset          |  2,948.4 μs |    275.23 μs |    758.07 μs |   2,926.8 μs |  109.3750 |  109.3750 |  109.3750 |   740.23 KB |
| ConcurrentSingleRequests |  2,822.6 μs |    298.86 μs |    802.86 μs |   2,542.7 μs |  140.6250 |   62.5000 |         - |   724.25 KB |
| ConcurrentSmallDatasets  | 14,773.1 μs |    912.82 μs |  2,498.83 μs |  14,252.8 μs |  500.0000 |  357.1429 |  285.7143 |  5346.45 KB |
| UpdateProduct            |    630.3 μs |     68.75 μs |    202.70 μs |     702.7 μs |    4.8828 |         - |         - |    21.42 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
