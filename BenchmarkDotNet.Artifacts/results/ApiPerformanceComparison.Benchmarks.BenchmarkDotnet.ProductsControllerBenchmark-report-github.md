```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev     | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-----------:|-----------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 19,116.21 μs | 380.633 μs | 438.338 μs | 19,091.68 μs |  656.2500 |  375.0000 | 125.0000 |  3514.69 KB |
| CreateProduct            |     88.58 μs |   1.463 μs |   2.234 μs |     88.05 μs |    4.8828 |    0.4883 |        - |    20.56 KB |
| DeleteProduct            |           NA |         NA |         NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 20,241.22 μs | 397.488 μs | 473.182 μs | 20,218.04 μs | 1406.2500 | 1375.0000 | 968.7500 | 10176.08 KB |
| GetSingleProduct         |     91.05 μs |   5.928 μs |  16.428 μs |     87.10 μs |    3.4180 |         - |        - |    15.09 KB |
| GetSmallDataset          |  3,350.72 μs | 115.462 μs | 333.135 μs |  3,250.23 μs |  156.2500 |   85.9375 |  70.3125 |   744.51 KB |
| ConcurrentSingleRequests |  1,087.26 μs |  21.274 μs |  40.987 μs |  1,078.83 μs |  140.6250 |   62.5000 |        - |   718.77 KB |
| ConcurrentSmallDatasets  |  5,679.34 μs | 230.520 μs | 679.693 μs |  5,795.54 μs |  515.6250 |  468.7500 | 343.7500 |  5271.45 KB |
| UpdateProduct            |    119.15 μs |   2.180 μs |   5.780 μs |    117.33 μs |    4.8828 |         - |        - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
