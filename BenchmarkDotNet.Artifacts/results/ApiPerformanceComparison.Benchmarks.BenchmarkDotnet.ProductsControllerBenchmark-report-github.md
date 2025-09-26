```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 11,408.79 μs |   721.724 μs | 1,938.864 μs | 10,752.12 μs |  222.2222 |               7.0000 |                - |         - |         - |  1423.19 KB |
| CreateProduct            |    107.89 μs |    14.895 μs |    40.523 μs |     87.37 μs |    4.8828 |               2.0010 |           0.0005 |    0.4883 |         - |    20.59 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |                   NA |               NA |        NA |        NA |          NA |
| GetMediumDataset         | 24,276.09 μs | 1,357.027 μs | 3,782.850 μs | 23,735.34 μs | 1333.3333 |             138.3333 |                - | 1222.2222 | 1000.0000 | 10134.63 KB |
| GetSingleProduct         |    103.19 μs |    20.998 μs |    59.567 μs |     73.14 μs |    3.6621 |               2.0002 |           0.0002 |         - |         - |    14.98 KB |
| GetSmallDataset          |  2,523.79 μs |    92.491 μs |   259.355 μs |  2,448.12 μs |  140.6250 |              13.9375 |                - |  140.6250 |  140.6250 |   715.64 KB |
| ConcurrentSingleRequests |  3,088.90 μs |   329.702 μs |   891.370 μs |  2,853.47 μs |  140.6250 |             100.1094 |           0.0313 |   46.8750 |         - |    718.9 KB |
| ConcurrentSmallDatasets  | 16,512.70 μs | 1,076.070 μs | 3,121.876 μs | 15,603.00 μs |  750.0000 |             135.4063 |           9.7500 |  562.5000 |  468.7500 |  5800.13 KB |
| UpdateProduct            |     80.39 μs |     2.956 μs |     7.890 μs |     76.48 μs |    4.8828 |               2.0000 |                - |    0.4883 |         - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
