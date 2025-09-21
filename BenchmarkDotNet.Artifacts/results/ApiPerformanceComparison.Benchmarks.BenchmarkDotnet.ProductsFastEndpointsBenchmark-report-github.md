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
| ColdStartSingleRequest   | 25,808.31 μs |   812.044 μs | 2,381.583 μs | 25,074.00 μs |  906.2500 |  375.0000 | 125.0000 |  5093.16 KB |
| CreateProduct            |     93.97 μs |     4.695 μs |    13.243 μs |     88.49 μs |    3.9063 |    0.4883 |        - |    16.55 KB |
| DeleteProduct            |           NA |           NA |           NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 24,442.91 μs | 1,353.781 μs | 3,884.252 μs | 22,454.91 μs | 1285.7143 | 1142.8571 | 857.1429 | 10171.35 KB |
| GetSingleProduct         |     51.39 μs |     1.267 μs |     3.615 μs |     51.18 μs |    2.9297 |         - |        - |    12.81 KB |
| GetSmallDataset          |  2,165.29 μs |    42.770 μs |    69.066 μs |  2,153.88 μs |  156.2500 |   93.7500 |  70.3125 |   725.66 KB |
| ConcurrentSingleRequests |    743.84 μs |    14.723 μs |    28.012 μs |    741.70 μs |  113.2813 |   82.0313 |        - |   606.32 KB |
| ConcurrentSmallDatasets  |  4,463.80 μs |   158.168 μs |   456.351 μs |  4,475.98 μs |  500.0000 |  453.1250 | 328.1250 |  5245.58 KB |
| UpdateProduct            |     85.93 μs |     2.825 μs |     7.828 μs |     83.57 μs |    3.9063 |         - |        - |     16.2 KB |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
