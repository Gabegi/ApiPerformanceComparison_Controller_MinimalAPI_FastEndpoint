```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev     | Median       | Gen0     | Gen1     | Gen2    | Allocated |
|------------------------- |-------------:|-----------:|-----------:|-------------:|---------:|---------:|--------:|----------:|
| ColdStartSingleRequest   | 13,903.46 μs | 258.828 μs | 540.270 μs | 13,728.70 μs | 250.0000 | 218.7500 | 31.2500 | 973.04 KB |
| CreateProduct            |     37.84 μs |   0.865 μs |   2.339 μs |     37.80 μs |   3.4180 |   0.4883 |       - |  14.55 KB |
| DeleteProduct            |           NA |         NA |         NA |           NA |       NA |       NA |      NA |        NA |
| GetMediumDataset         |    287.53 μs |   5.724 μs |  11.298 μs |    292.53 μs |  14.6484 |   1.9531 |       - |  62.93 KB |
| GetSingleProduct         |           NA |         NA |         NA |           NA |       NA |       NA |      NA |        NA |
| GetSmallDataset          |    278.92 μs |   5.854 μs |  16.511 μs |    284.53 μs |  14.6484 |        - |       - |  63.05 KB |
| ConcurrentSingleRequests |           NA |         NA |         NA |           NA |       NA |       NA |      NA |        NA |
| ConcurrentSmallDatasets  |    572.32 μs |   6.448 μs |   9.248 μs |    571.59 μs | 109.3750 |  48.8281 |       - | 475.24 KB |
| UpdateProduct            |     39.15 μs |   1.606 μs |   4.370 μs |     38.36 μs |   3.4180 |        - |       - |  14.58 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
