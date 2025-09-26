```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated  |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|-----------:|
| ColdStartSingleRequest   | 13,259.24 μs |   818.935 μs |  2,214.04 μs | 12,593.92 μs |  156.2500 |               7.0000 |                - |   93.7500 |         - |  903.35 KB |
| CreateProduct            |    307.20 μs |    53.012 μs |    156.31 μs |    329.54 μs |    3.4180 |               2.0010 |           0.0020 |    0.4883 |         - |   14.56 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |                   NA |               NA |        NA |        NA |         NA |
| GetMediumDataset         | 31,265.59 μs | 4,399.349 μs | 11,968.75 μs | 28,115.76 μs | 1333.3333 |             123.8333 |                - | 1166.6667 | 1000.0000 | 10029.1 KB |
| GetSingleProduct         |     66.31 μs |     6.793 μs |     18.60 μs |     64.91 μs |    2.6855 |               2.0000 |           0.0164 |         - |         - |   11.32 KB |
| GetSmallDataset          |  2,455.35 μs |    62.987 μs |    179.70 μs |  2,386.49 μs |  195.3125 |              15.6797 |                - |  101.5625 |  101.5625 |  817.49 KB |
| ConcurrentSingleRequests |  1,971.01 μs |    95.897 μs |    257.62 μs |  1,900.07 μs |  109.3750 |             100.4219 |           0.0156 |   31.2500 |         - |  531.31 KB |
| ConcurrentSmallDatasets  | 23,173.12 μs | 1,330.170 μs |  3,880.17 μs | 21,829.93 μs |  875.0000 |             132.7500 |           9.2813 |  687.5000 |  500.0000 | 5859.57 KB |
| UpdateProduct            |           NA |           NA |           NA |           NA |        NA |                   NA |               NA |        NA |        NA |         NA |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.UpdateProduct: .NET 9.0(Runtime=.NET 9.0)
