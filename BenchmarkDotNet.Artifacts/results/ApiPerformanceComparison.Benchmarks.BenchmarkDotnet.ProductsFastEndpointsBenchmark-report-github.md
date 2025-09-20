```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev      | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-------------:|------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 30,202.36 μs | 2,828.108 μs | 8,068.75 μs | 27,193.72 μs |  833.3333 |  333.3333 |  83.3333 |  5132.34 KB |
| CreateProduct            |     74.91 μs |     4.166 μs |    10.98 μs |     71.36 μs |    3.9063 |    0.4883 |        - |    16.55 KB |
| DeleteProduct            |           NA |           NA |          NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 26,695.18 μs | 1,776.416 μs | 4,862.91 μs | 25,770.07 μs | 1285.7143 | 1142.8571 | 857.1429 | 10257.63 KB |
| GetSingleProduct         |     64.73 μs |    11.444 μs |    32.46 μs |     51.62 μs |    2.9297 |         - |        - |    12.79 KB |
| GetSmallDataset          |  2,164.31 μs |    42.826 μs |   100.95 μs |  2,180.67 μs |  156.2500 |  101.5625 |  78.1250 |   731.32 KB |
| ConcurrentSingleRequests |    729.04 μs |    13.752 μs |    21.41 μs |    725.19 μs |  109.3750 |   82.0313 |        - |   606.38 KB |
| ConcurrentSmallDatasets  | 13,658.18 μs |   750.567 μs | 2,104.67 μs | 13,308.95 μs |  466.6667 |  400.0000 | 333.3333 |  5317.23 KB |
| UpdateProduct            |     88.65 μs |    12.383 μs |    33.90 μs |     72.24 μs |    3.9063 |         - |        - |     16.2 KB |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
