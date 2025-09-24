```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 12,792.75 μs | 253.698 μs |   668.342 μs | 12,617.13 μs |  250.0000 |               7.0000 |                - |  125.0000 |         - |  1417.15 KB |
| CreateProduct            |    107.21 μs |   2.140 μs |     5.330 μs |    106.09 μs |    4.8828 |               2.0010 |           0.0010 |    0.4883 |         - |    20.59 KB |
| DeleteProduct            |    174.01 μs |   3.697 μs |    10.120 μs |    171.29 μs |    7.8125 |               4.0010 |                - |         - |         - |    34.34 KB |
| GetMediumDataset         | 24,809.24 μs | 464.182 μs | 1,309.234 μs | 24,310.25 μs | 1468.7500 |             137.3125 |           0.0313 | 1406.2500 | 1000.0000 | 10222.88 KB |
| GetSingleProduct         |     85.83 μs |   1.701 μs |     4.329 μs |     84.50 μs |    3.4180 |               2.0005 |                - |         - |         - |    15.08 KB |
| GetSmallDataset          |  3,223.47 μs |  63.485 μs |   117.674 μs |  3,217.00 μs |  125.0000 |              15.9141 |           0.0078 |  109.3750 |  109.3750 |   818.55 KB |
| ConcurrentSingleRequests |    869.99 μs |  17.180 μs |    41.162 μs |    865.59 μs |  144.5313 |             100.0430 |           0.0234 |   66.4063 |         - |   718.78 KB |
| ConcurrentSmallDatasets  |  6,402.67 μs | 122.995 μs |   248.455 μs |  6,351.68 μs |  914.0625 |             139.7656 |           9.0234 |  679.6875 |  476.5625 |  5762.68 KB |
| UpdateProduct            |    114.29 μs |   2.641 μs |     7.096 μs |    112.40 μs |    4.8828 |               2.0000 |                - |         - |         - |    21.36 KB |
