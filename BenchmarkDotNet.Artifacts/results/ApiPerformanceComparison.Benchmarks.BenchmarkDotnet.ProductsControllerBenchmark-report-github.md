```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 18,199.63 μs | 537.588 μs | 1,444.196 μs | 17,748.34 μs |  593.7500 |  375.0000 |  93.7500 |  3394.71 KB |
| CreateProduct            |     94.64 μs |   2.542 μs |     7.002 μs |     92.12 μs |    4.8828 |    0.4883 |        - |    20.55 KB |
| DeleteProduct            |           NA |         NA |           NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 23,078.26 μs | 651.105 μs | 1,868.143 μs | 22,390.37 μs | 1333.3333 | 1222.2222 | 888.8889 | 10181.85 KB |
| GetSingleProduct         |     80.03 μs |   3.108 μs |     8.507 μs |     77.37 μs |    3.4180 |         - |        - |    15.08 KB |
| GetSmallDataset          |  2,332.86 μs |  42.978 μs |    96.126 μs |  2,315.32 μs |  164.0625 |   93.7500 |  70.3125 |   734.83 KB |
| ConcurrentSingleRequests |    813.17 μs |  15.562 μs |    33.498 μs |    806.04 μs |  144.5313 |   62.5000 |        - |   718.87 KB |
| ConcurrentSmallDatasets  |  4,276.69 μs |  85.453 μs |   211.219 μs |  4,214.95 μs |  546.8750 |  437.5000 | 359.3750 |  5268.98 KB |
| UpdateProduct            |    100.92 μs |   1.580 μs |     2.412 μs |    100.41 μs |    4.8828 |         - |        - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
