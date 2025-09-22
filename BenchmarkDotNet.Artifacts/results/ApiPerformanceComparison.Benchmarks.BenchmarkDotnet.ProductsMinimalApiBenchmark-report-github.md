```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Gen0     | Gen1     | Gen2     | Allocated  |
|------------------------- |-------------:|-----------:|-------------:|---------:|---------:|---------:|-----------:|
| ColdStartSingleRequest   | 26,174.34 μs | 678.303 μs | 1,856.843 μs | 656.2500 | 375.0000 | 125.0000 | 3579.59 KB |
| CreateProduct            |     69.62 μs |   3.015 μs |     8.203 μs |   3.4180 |   0.4883 |        - |   14.58 KB |
| DeleteProduct            |           NA |         NA |           NA |       NA |       NA |       NA |         NA |
| GetMediumDataset         |    348.39 μs |   5.450 μs |     9.401 μs |  15.6250 |        - |        - |   64.59 KB |
| GetSingleProduct         |           NA |         NA |           NA |       NA |       NA |       NA |         NA |
| GetSmallDataset          |    360.46 μs |   6.773 μs |    17.960 μs |  15.6250 |   1.9531 |        - |   64.04 KB |
| ConcurrentSingleRequests |           NA |         NA |           NA |       NA |       NA |       NA |         NA |
| ConcurrentSmallDatasets  |    701.24 μs |  20.229 μs |    56.055 μs | 101.5625 |  46.8750 |        - |  451.25 KB |
| UpdateProduct            |     78.44 μs |   3.420 μs |     9.645 μs |   3.4180 |        - |        - |   14.61 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
