```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev        | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|--------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 10,460.78 μs |   378.238 μs |  1,060.621 μs | 10,062.77 μs |  203.1250 |               7.0000 |                - |  125.0000 |   15.6250 |   904.47 KB |
| CreateProduct            |     42.99 μs |     2.860 μs |      7.780 μs |     42.06 μs |    3.4180 |               2.0000 |           0.0005 |    0.4883 |         - |    14.55 KB |
| DeleteProduct            |           NA |           NA |            NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |
| GetMediumDataset         | 42,288.00 μs | 6,842.195 μs | 19,186.249 μs | 33,402.29 μs | 1500.0000 |             139.6250 |                - | 1375.0000 | 1000.0000 | 10226.98 KB |
| GetSingleProduct         |     44.57 μs |     5.280 μs |     14.184 μs |     40.28 μs |    2.6855 |               2.0002 |                - |         - |         - |    11.27 KB |
| GetSmallDataset          |  2,398.54 μs |    82.058 μs |    226.011 μs |  2,322.75 μs |  125.0000 |              14.9688 |                - |  109.3750 |  109.3750 |   804.68 KB |
| ConcurrentSingleRequests |  2,270.84 μs |   185.539 μs |    504.773 μs |  2,170.17 μs |  109.3750 |             100.1094 |                - |   46.8750 |         - |   530.98 KB |
| ConcurrentSmallDatasets  | 18,963.88 μs | 1,541.591 μs |  4,496.893 μs | 18,266.01 μs |  750.0000 |             104.9375 |           8.8125 |  562.5000 |  437.5000 |  5991.68 KB |
| UpdateProduct            |           NA |           NA |            NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsMinimalApiBenchmark.UpdateProduct: .NET 9.0(Runtime=.NET 9.0)
