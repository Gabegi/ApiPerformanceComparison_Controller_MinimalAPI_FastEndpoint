```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev     | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-----------:|-----------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 11,351.87 μs | 226.881 μs | 556.544 μs |  312.5000 |  156.2500 |  31.2500 |  1409.05 KB |
| CreateProduct            |     95.75 μs |   1.092 μs |   1.854 μs |    4.8828 |    0.4883 |        - |    20.56 KB |
| DeleteProduct            |           NA |         NA |         NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 20,673.55 μs | 228.622 μs | 213.853 μs | 1406.2500 | 1343.7500 | 937.5000 | 10183.07 KB |
| GetSingleProduct         |     75.52 μs |   1.173 μs |   2.423 μs |    3.4180 |         - |        - |    15.09 KB |
| GetSmallDataset          |  2,534.62 μs |  36.960 μs |  34.572 μs |  109.3750 |  109.3750 | 109.3750 |   733.97 KB |
| ConcurrentSingleRequests |    755.36 μs |  11.957 μs |  19.309 μs |  144.5313 |   66.4063 |        - |   718.82 KB |
| ConcurrentSmallDatasets  |  4,428.13 μs |  88.030 μs | 171.695 μs |  531.2500 |  468.7500 | 351.5625 |  5264.31 KB |
| UpdateProduct            |     98.64 μs |   1.454 μs |   2.220 μs |    4.8828 |         - |        - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
