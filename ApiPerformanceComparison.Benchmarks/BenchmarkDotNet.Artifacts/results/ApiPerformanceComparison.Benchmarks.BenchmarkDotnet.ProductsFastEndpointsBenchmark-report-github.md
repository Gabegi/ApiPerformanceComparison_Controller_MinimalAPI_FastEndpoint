```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev      | Median       | Gen0      | Gen1      | Gen2      | Allocated  |
|------------------------- |-------------:|-------------:|------------:|-------------:|----------:|----------:|----------:|-----------:|
| ColdStartSingleRequest   | 35,097.18 μs | 2,815.901 μs | 8,302.75 μs | 33,530.07 μs |  363.6364 |         - |         - | 2672.45 KB |
| CreateProduct            |    990.85 μs |   132.824 μs |   381.10 μs |    913.75 μs |    3.9063 |         - |         - |   16.83 KB |
| DeleteProduct            |           NA |           NA |          NA |           NA |        NA |        NA |        NA |         NA |
| GetMediumDataset         | 31,386.89 μs | 1,335.387 μs | 3,831.47 μs | 31,017.80 μs | 1428.5714 | 1285.7143 | 1000.0000 | 10254.2 KB |
| GetSingleProduct         |     73.21 μs |     6.448 μs |    17.87 μs |     66.80 μs |    2.9297 |         - |         - |   12.42 KB |
| GetSmallDataset          |  3,431.47 μs |    53.436 μs |   104.22 μs |  3,399.77 μs |  109.3750 |  109.3750 |  109.3750 |  727.92 KB |
| ConcurrentSingleRequests |    977.26 μs |    32.051 μs |    90.92 μs |    962.79 μs |  121.0938 |   39.0625 |         - |  587.11 KB |
| ConcurrentSmallDatasets  |  5,969.46 μs |   188.199 μs |   548.99 μs |  5,977.93 μs |  500.0000 |  421.8750 |  312.5000 | 5311.04 KB |
| UpdateProduct            |    126.37 μs |     8.970 μs |    24.86 μs |    118.53 μs |    3.9063 |         - |         - |   16.38 KB |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
