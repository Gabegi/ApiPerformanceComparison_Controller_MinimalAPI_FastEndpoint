```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev     | Gen0     | Gen1     | Gen2    | Allocated |
|------------------------- |-------------:|-----------:|-----------:|---------:|---------:|--------:|----------:|
| MinimalApi_ColdStart     | 11,917.74 μs | 235.040 μs | 631.421 μs | 171.8750 | 140.6250 | 15.6250 | 919.53 KB |
| CreateProduct            |  1,154.16 μs | 224.713 μs | 662.572 μs |  28.8086 |  25.3906 | 25.3906 | 835.15 KB |
| DeleteProduct            |    120.02 μs |   5.787 μs |  16.697 μs |   5.8594 |        - |       - |  25.32 KB |
| GetMediumDataset         |    302.08 μs |   7.750 μs |  22.112 μs |  14.6484 |        - |       - |  63.42 KB |
| GetSingleProduct         |           NA |         NA |         NA |       NA |       NA |      NA |        NA |
| GetSmallDataset          |    293.07 μs |   7.570 μs |  21.474 μs |  15.6250 |        - |       - |  64.58 KB |
| ConcurrentSingleRequests |           NA |         NA |         NA |       NA |       NA |      NA |        NA |
| ConcurrentSmallDatasets  |    613.99 μs |  12.120 μs |  32.140 μs | 107.4219 |  41.0156 |       - | 470.96 KB |
| UpdateProduct            |     46.13 μs |   1.228 μs |   3.383 μs |   3.4180 |        - |       - |  14.59 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
