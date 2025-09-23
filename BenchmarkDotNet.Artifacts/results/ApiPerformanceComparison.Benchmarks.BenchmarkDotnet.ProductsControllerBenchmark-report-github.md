```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated  |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|-----------:|
| ColdStartSingleRequest   | 13,165.23 μs | 561.581 μs | 1,620.290 μs | 12,942.66 μs |  250.0000 |               7.0000 |                - |  125.0000 |         - | 1417.26 KB |
| CreateProduct            |     97.92 μs |   4.193 μs |    11.046 μs |     93.62 μs |    4.8828 |               2.0000 |                - |         - |         - |   20.59 KB |
| DeleteProduct            |    150.05 μs |   2.535 μs |     5.063 μs |    148.18 μs |    7.8125 |               4.0000 |           0.0010 |         - |         - |   34.33 KB |
| GetMediumDataset         | 23,232.00 μs | 806.777 μs | 2,327.736 μs | 21,951.08 μs | 1468.7500 |             136.0938 |           0.0313 | 1375.0000 | 1000.0000 | 10223.3 KB |
| GetSingleProduct         |     78.19 μs |   2.451 μs |     6.626 μs |     75.29 μs |    3.4180 |               2.0000 |                - |         - |         - |   15.09 KB |
| GetSmallDataset          |  2,824.29 μs |  53.592 μs |    44.752 μs |  2,821.48 μs |  140.6250 |              15.6719 |                - |  109.3750 |  109.3750 |  810.58 KB |
| ConcurrentSingleRequests |    792.97 μs |  14.566 μs |    31.973 μs |    785.01 μs |  140.6250 |             100.0469 |           0.0156 |   70.3125 |         - |  718.82 KB |
| ConcurrentSmallDatasets  |  5,738.64 μs | 114.668 μs |   295.994 μs |  5,644.53 μs |  765.6250 |             135.8906 |           9.1250 |  585.9375 |  359.3750 | 5806.45 KB |
| UpdateProduct            |    100.71 μs |   1.286 μs |     1.965 μs |    100.08 μs |    4.8828 |               2.0005 |           0.0010 |         - |         - |   21.36 KB |
