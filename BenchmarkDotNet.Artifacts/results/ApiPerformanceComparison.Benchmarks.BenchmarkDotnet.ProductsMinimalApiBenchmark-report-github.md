```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 21,029.89 μs | 475.287 μs | 1,317.019 μs | 20,713.96 μs |  531.2500 |  343.7500 |   93.7500 |  2946.54 KB |
| CreateProduct            |     62.64 μs |   4.427 μs |    12.415 μs |     57.86 μs |    3.4180 |    0.4883 |         - |    14.54 KB |
| DeleteProduct            |           NA |         NA |           NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 22,986.08 μs | 306.057 μs |   286.286 μs | 22,963.09 μs | 1437.5000 | 1406.2500 | 1000.0000 | 10172.87 KB |
| GetSingleProduct         |     32.19 μs |   2.588 μs |     7.256 μs |     29.91 μs |    2.6855 |         - |         - |    11.27 KB |
| GetSmallDataset          |  2,182.74 μs |  37.094 μs |    70.575 μs |  2,156.24 μs |  164.0625 |   93.7500 |   78.1250 |   719.86 KB |
| ConcurrentSingleRequests |    705.32 μs |  13.438 μs |    32.454 μs |    703.66 μs |  109.3750 |   42.9688 |         - |   531.37 KB |
| ConcurrentSmallDatasets  |  4,481.61 μs |  89.769 μs |   257.565 μs |  4,437.58 μs |  531.2500 |  453.1250 |  359.3750 |  5235.51 KB |
| UpdateProduct            |     65.35 μs |   3.623 μs |    10.099 μs |     65.19 μs |    3.4180 |         - |         - |    14.61 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
