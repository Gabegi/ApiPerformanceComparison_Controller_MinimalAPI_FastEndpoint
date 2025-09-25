```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 12,773.70 μs | 548.980 μs | 1,557.365 μs |  250.0000 |               7.0000 |           0.0313 |  125.0000 |         - |  1416.25 KB |
| CreateProduct            |    117.49 μs |   6.025 μs |    16.289 μs |    4.8828 |               2.0000 |           0.0010 |         - |         - |    20.59 KB |
| DeleteProduct            |           NA |         NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |
| GetMediumDataset         | 24,875.17 μs | 495.793 μs | 1,348.842 μs | 1500.0000 |             138.3333 |                - | 1333.3333 | 1000.0000 | 10335.15 KB |
| GetSingleProduct         |     77.94 μs |   1.377 μs |     2.621 μs |    3.4180 |               2.0005 |           0.0010 |         - |         - |    15.09 KB |
| GetSmallDataset          |  3,006.87 μs | 125.840 μs |   348.703 μs |  195.3125 |              16.0234 |                - |  125.0000 |   93.7500 |   821.24 KB |
| ConcurrentSingleRequests |    816.01 μs |  16.242 μs |    41.633 μs |  144.5313 |             100.0195 |           0.0313 |   66.4063 |         - |   718.77 KB |
| ConcurrentSmallDatasets  |  6,509.80 μs | 159.284 μs |   446.650 μs |  687.5000 |             118.4844 |           9.2344 |  500.0000 |  296.8750 |   5947.8 KB |
| UpdateProduct            |           NA |         NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsControllerBenchmark.UpdateProduct: .NET 9.0(Runtime=.NET 9.0)
