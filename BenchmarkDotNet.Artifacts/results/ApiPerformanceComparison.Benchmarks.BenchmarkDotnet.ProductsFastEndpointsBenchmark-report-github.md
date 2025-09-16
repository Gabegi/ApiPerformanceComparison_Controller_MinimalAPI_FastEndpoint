```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev        | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|--------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 39,755.36 μs | 8,505.661 μs | 24,404.338 μs |  333.3333 |         - |         - |  2677.31 KB |
| CreateProduct            |  1,975.34 μs |   355.942 μs |    986.313 μs |    3.9063 |    0.4883 |         - |    16.84 KB |
| DeleteProduct            |           NA |           NA |            NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 34,184.64 μs | 1,290.584 μs |  3,467.069 μs | 1428.5714 | 1285.7143 | 1000.0000 | 10271.11 KB |
| GetSingleProduct         |     77.01 μs |     2.140 μs |      5.929 μs |    2.9297 |         - |         - |    12.39 KB |
| GetSmallDataset          |  2,038.06 μs |    39.993 μs |    106.750 μs |  109.3750 |  109.3750 |  109.3750 |   739.01 KB |
| ConcurrentSingleRequests |  3,441.19 μs |   114.925 μs |    300.738 μs |  125.0000 |   31.2500 |         - |   587.17 KB |
| ConcurrentSmallDatasets  | 22,352.51 μs |   798.133 μs |  2,116.535 μs |  500.0000 |  375.0000 |  375.0000 |  5308.79 KB |
| UpdateProduct            |     75.29 μs |     2.581 μs |      7.321 μs |    3.9063 |         - |         - |    16.38 KB |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
