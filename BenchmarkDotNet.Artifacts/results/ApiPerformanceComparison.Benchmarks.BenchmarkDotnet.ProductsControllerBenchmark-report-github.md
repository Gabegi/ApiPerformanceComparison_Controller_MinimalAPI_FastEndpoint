```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 24,718.30 μs | 2,623.525 μs | 7,569.471 μs | 24,324.33 μs |  583.3333 |  250.0000 |  83.3333 |  3406.86 KB |
| CreateProduct            |     78.00 μs |     2.340 μs |     6.206 μs |     75.40 μs |    4.8828 |    0.4883 |        - |    20.55 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 33,416.92 μs | 1,752.416 μs | 4,913.962 μs | 32,714.85 μs | 1250.0000 | 1125.0000 | 875.0000 | 10192.08 KB |
| GetSingleProduct         |     66.30 μs |     1.370 μs |     3.751 μs |     65.53 μs |    3.6621 |         - |        - |    15.09 KB |
| GetSmallDataset          |  1,987.09 μs |    41.807 μs |   112.310 μs |  1,952.65 μs |  164.0625 |   70.3125 |  70.3125 |   732.51 KB |
| ConcurrentSingleRequests |  3,983.04 μs |   342.024 μs |   924.682 μs |  3,610.70 μs |  140.6250 |   62.5000 |        - |   718.99 KB |
| ConcurrentSmallDatasets  | 23,142.51 μs |   883.874 μs | 2,249.741 μs | 23,087.27 μs |  500.0000 |  375.0000 | 375.0000 |   5267.5 KB |
| UpdateProduct            |     80.71 μs |     1.703 μs |     4.576 μs |     79.96 μs |    4.8828 |         - |        - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
