```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 24,193.30 μs | 2,108.389 μs | 6,116.818 μs | 21,946.91 μs |  500.0000 |  250.0000 |  62.5000 |  2928.58 KB |
| CreateProduct            |     35.34 μs |     2.475 μs |     6.733 μs |     32.88 μs |    3.4180 |    0.7324 |        - |    14.58 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 25,716.73 μs | 1,489.729 μs | 4,298.210 μs | 25,108.44 μs | 1363.6364 | 1272.7273 | 909.0909 | 10256.38 KB |
| GetSingleProduct         |     66.28 μs |    12.760 μs |    37.020 μs |     54.05 μs |    2.6855 |         - |        - |    11.39 KB |
| GetSmallDataset          |  2,165.94 μs |    81.289 μs |   229.277 μs |  2,174.65 μs |  164.0625 |  101.5625 |  78.1250 |   738.45 KB |
| ConcurrentSingleRequests |  2,190.05 μs |   173.785 μs |   463.867 μs |  2,070.50 μs |  109.3750 |   31.2500 |        - |    534.5 KB |
| ConcurrentSmallDatasets  | 15,746.02 μs |   994.269 μs | 2,836.706 μs | 15,658.79 μs |  533.3333 |  400.0000 | 333.3333 |  5313.38 KB |
| UpdateProduct            |     50.74 μs |     7.073 μs |    19.121 μs |     44.54 μs |    3.4180 |         - |        - |    14.64 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
