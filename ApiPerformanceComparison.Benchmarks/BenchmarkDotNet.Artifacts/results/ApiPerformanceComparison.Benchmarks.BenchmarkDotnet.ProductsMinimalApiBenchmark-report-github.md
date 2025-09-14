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
| ColdStartSingleRequest   | 14,594.44 μs |   428.930 μs | 1,195.685 μs | 14,190.44 μs |  156.2500 |   93.7500 |         - |   849.96 KB |
| CreateProduct            |  2,777.31 μs |   556.880 μs | 1,641.972 μs |  2,790.77 μs |    3.4180 |    0.4883 |         - |     14.6 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 32,979.49 μs |   793.014 μs | 2,236.710 μs | 32,392.54 μs | 1500.0000 | 1166.6667 | 1000.0000 | 10213.43 KB |
| GetSingleProduct         |     65.46 μs |     3.444 μs |     9.312 μs |     63.54 μs |    2.4414 |         - |         - |    11.39 KB |
| GetSmallDataset          | 33,124.27 μs |   899.459 μs | 2,580.716 μs | 32,623.94 μs | 1428.5714 | 1285.7143 | 1000.0000 |  10202.7 KB |
| ConcurrentSingleRequests |  1,010.56 μs |    19.614 μs |    39.172 μs |    996.11 μs |  113.2813 |   42.9688 |         - |   535.27 KB |
| ConcurrentSmallDatasets  | 44,693.08 μs | 1,180.903 μs | 3,330.757 μs | 44,460.21 μs | 1200.0000 | 1000.0000 | 1000.0000 | 45793.84 KB |
| UpdateProduct            |    103.99 μs |     4.571 μs |    12.589 μs |     99.41 μs |    3.4180 |         - |         - |    14.71 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
