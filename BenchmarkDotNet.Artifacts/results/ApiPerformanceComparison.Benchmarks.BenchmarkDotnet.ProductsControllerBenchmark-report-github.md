```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 12,715.88 μs | 252.501 μs |   624.121 μs | 12,580.28 μs |  281.2500 |  187.5000 |         - |  1408.16 KB |
| CreateProduct            |    111.67 μs |   4.089 μs |    11.399 μs |    105.85 μs |    4.8828 |    0.4883 |         - |    20.55 KB |
| DeleteProduct            |    172.16 μs |   3.953 μs |    10.888 μs |    170.64 μs |    7.8125 |         - |         - |    34.31 KB |
| GetMediumDataset         | 23,036.10 μs | 559.796 μs | 1,588.047 μs | 22,817.78 μs | 1468.7500 | 1343.7500 | 1000.0000 | 10185.02 KB |
| GetSingleProduct         |     77.12 μs |   1.235 μs |     2.861 μs |     76.45 μs |    3.4180 |         - |         - |    15.09 KB |
| GetSmallDataset          |  2,685.68 μs |  52.794 μs |    83.737 μs |  2,654.62 μs |  109.3750 |  109.3750 |  109.3750 |   731.98 KB |
| ConcurrentSingleRequests |    808.01 μs |  16.084 μs |    40.352 μs |    793.37 μs |  144.5313 |   66.4063 |         - |   718.82 KB |
| ConcurrentSmallDatasets  |  4,680.86 μs |  93.506 μs |   247.964 μs |  4,629.27 μs |  546.8750 |  492.1875 |  367.1875 |  5268.79 KB |
| UpdateProduct            |    101.38 μs |   1.241 μs |     2.107 μs |    100.78 μs |    4.8828 |         - |         - |    21.36 KB |
