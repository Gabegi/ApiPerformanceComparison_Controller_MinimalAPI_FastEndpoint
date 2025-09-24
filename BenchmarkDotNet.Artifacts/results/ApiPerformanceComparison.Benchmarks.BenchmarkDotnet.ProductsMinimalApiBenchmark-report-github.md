```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev     | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-----------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 13,824.95 μs | 300.236 μs | 880.540 μs | 13,617.85 μs |  187.5000 |               7.0000 |                - |  125.0000 |         - |   906.08 KB |
| CreateProduct            |     53.42 μs |   1.806 μs |   4.912 μs |     53.10 μs |    3.4180 |               2.0000 |           0.0029 |    0.4883 |         - |    14.57 KB |
| DeleteProduct            |    117.88 μs |   2.195 μs |   4.484 μs |    116.97 μs |    5.8594 |               4.0000 |           0.0020 |         - |         - |    24.84 KB |
| GetMediumDataset         | 23,853.72 μs | 349.839 μs | 273.132 μs | 23,856.66 μs | 1468.7500 |             134.9688 |                - | 1406.2500 | 1000.0000 | 10049.24 KB |
| GetSingleProduct         |     33.39 μs |   1.345 μs |   3.860 μs |     32.56 μs |    2.6855 |               2.0002 |           0.0027 |         - |         - |    11.27 KB |
| GetSmallDataset          |  3,103.03 μs |  61.455 μs | 119.862 μs |  3,096.41 μs |  117.1875 |              14.6641 |                - |  109.3750 |  109.3750 |   805.69 KB |
| ConcurrentSingleRequests |    714.40 μs |  14.219 μs |  32.671 μs |    709.60 μs |  109.3750 |             100.2852 |           0.0234 |   50.7813 |         - |   531.13 KB |
| ConcurrentSmallDatasets  |  6,231.30 μs | 124.045 μs | 241.941 μs |  6,169.89 μs |  789.0625 |             128.9219 |           8.8203 |  757.8125 |  453.1250 |  5768.23 KB |
| UpdateProduct            |     78.49 μs |   4.140 μs |  11.542 μs |     74.76 μs |    3.4180 |               2.0005 |           0.0059 |         - |         - |    14.61 KB |
