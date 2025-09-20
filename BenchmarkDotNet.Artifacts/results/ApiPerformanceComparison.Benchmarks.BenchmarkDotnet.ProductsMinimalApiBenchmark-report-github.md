```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error         | StdDev       | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|--------------:|-------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 46,265.90 μs | 13,936.178 μs | 38,617.09 μs | 28,472.42 μs |  416.6667 |  250.0000 |        - |  2934.15 KB |
| CreateProduct            |     79.51 μs |     18.517 μs |     49.43 μs |     59.52 μs |    3.4180 |    0.7324 |        - |    14.58 KB |
| DeleteProduct            |           NA |            NA |           NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 30,647.89 μs |  1,670.763 μs |  4,629.68 μs | 30,719.11 μs | 1333.3333 | 1222.2222 | 888.8889 | 10181.88 KB |
| GetSingleProduct         |     47.88 μs |      4.776 μs |     13.39 μs |     44.54 μs |    2.6855 |         - |        - |    11.34 KB |
| GetSmallDataset          |  1,925.25 μs |     63.377 μs |    175.62 μs |  1,873.99 μs |  164.0625 |  101.5625 |  78.1250 |   727.28 KB |
| ConcurrentSingleRequests |  3,279.70 μs |    182.792 μs |    491.06 μs |  3,091.51 μs |  109.3750 |   46.8750 |        - |   534.79 KB |
| ConcurrentSmallDatasets  | 23,817.30 μs |    542.635 μs |  1,419.98 μs | 23,647.10 μs |  375.0000 |  250.0000 | 250.0000 |  5078.76 KB |
| UpdateProduct            |    237.92 μs |     62.067 μs |    183.01 μs |    175.35 μs |    3.4180 |         - |        - |    14.69 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
