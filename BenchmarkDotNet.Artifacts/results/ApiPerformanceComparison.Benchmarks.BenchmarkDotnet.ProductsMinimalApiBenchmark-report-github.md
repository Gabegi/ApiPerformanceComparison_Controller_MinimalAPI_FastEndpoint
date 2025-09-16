```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev        | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-------------:|--------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 144,828.3 μs | 43,380.80 μs | 124,467.66 μs | 75,037.20 μs | 3000.0000 | 1400.0000 | 200.0000 | 18859.39 KB |
| CreateProduct            |   1,025.0 μs |    198.46 μs |     566.21 μs |  1,029.48 μs |    2.9297 |         - |        - |    14.74 KB |
| DeleteProduct            |           NA |           NA |            NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         |  34,568.5 μs |  2,366.20 μs |   6,477.44 μs | 33,802.14 μs |  600.0000 |  400.0000 | 200.0000 | 10243.17 KB |
| GetSingleProduct         |     131.2 μs |     25.19 μs |      73.09 μs |    118.47 μs |    2.6855 |         - |        - |    11.48 KB |
| GetSmallDataset          |   9,330.1 μs |  1,632.32 μs |   4,683.44 μs | 10,479.36 μs |   71.4286 |         - |        - |   734.99 KB |
| ConcurrentSingleRequests |   2,611.3 μs |    249.13 μs |     677.77 μs |  2,477.06 μs |  109.3750 |   31.2500 |        - |   541.22 KB |
| ConcurrentSmallDatasets  |  19,007.3 μs |    614.43 μs |   1,671.59 μs | 18,784.33 μs |  200.0000 |  100.0000 | 100.0000 |  5211.96 KB |
| UpdateProduct            |     110.5 μs |     19.00 μs |      53.90 μs |     89.51 μs |    3.4180 |         - |        - |    14.81 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
