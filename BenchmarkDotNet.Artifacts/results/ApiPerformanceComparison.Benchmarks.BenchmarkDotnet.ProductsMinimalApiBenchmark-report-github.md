```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev     | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-----------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 11,167.86 μs | 223.030 μs | 378.722 μs | 11,109.82 μs |  156.2500 |               7.0000 |                - |  125.0000 |         - |    902.6 KB |
| CreateProduct            |     38.12 μs |   0.883 μs |   2.433 μs |     37.94 μs |    3.4180 |               2.0005 |           0.0010 |    0.4883 |         - |    14.55 KB |
| DeleteProduct            |           NA |         NA |         NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |
| GetMediumDataset         | 20,645.38 μs | 187.425 μs | 175.317 μs | 20,621.77 μs | 1468.7500 |             140.3438 |                - | 1437.5000 | 1000.0000 | 10256.98 KB |
| GetSingleProduct         |     24.89 μs |   0.881 μs |   2.398 μs |     24.12 μs |    2.6855 |               2.0000 |           0.0002 |         - |         - |    11.27 KB |
| GetSmallDataset          |  3,219.54 μs | 102.167 μs | 291.488 μs |  3,134.09 μs |  125.0000 |              14.6953 |                - |  109.3750 |  109.3750 |   801.02 KB |
| ConcurrentSingleRequests |    670.51 μs |   8.504 μs |  11.352 μs |    672.04 μs |  109.3750 |             100.1211 |           0.0078 |   46.8750 |         - |   530.99 KB |
| ConcurrentSmallDatasets  |  5,785.11 μs |  37.044 μs |  45.493 μs |  5,770.21 μs |  851.5625 |             132.3672 |           8.7969 |  835.9375 |  531.2500 |  5824.07 KB |
| UpdateProduct            |           NA |         NA |         NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.UpdateProduct: .NET 9.0(Runtime=.NET 9.0)
