```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 12,385.20 μs |   336.248 μs |   970.153 μs | 12,401.17 μs |  156.2500 |               7.0000 |                - |  125.0000 |         - |    902.8 KB |
| CreateProduct            |     57.84 μs |     1.902 μs |     5.175 μs |     57.85 μs |    3.4180 |               2.0000 |           0.0054 |    0.4883 |         - |    14.57 KB |
| DeleteProduct            |    118.68 μs |     2.364 μs |     6.103 μs |    117.39 μs |    5.8594 |               4.0000 |                - |         - |         - |    24.85 KB |
| GetMediumDataset         | 23,729.96 μs | 1,096.319 μs | 3,163.132 μs | 22,743.03 μs | 1468.7500 |             139.0938 |           0.0625 | 1437.5000 | 1000.0000 | 10260.98 KB |
| GetSingleProduct         |     31.97 μs |     3.434 μs |     9.629 μs |     27.04 μs |    2.6855 |               2.0002 |           0.0012 |         - |         - |    11.27 KB |
| GetSmallDataset          |  2,857.15 μs |   107.322 μs |   309.649 μs |  2,796.08 μs |  117.1875 |              14.7656 |           0.0078 |  109.3750 |  109.3750 |   808.64 KB |
| ConcurrentSingleRequests |    648.44 μs |    12.823 μs |    23.769 μs |    642.15 μs |  105.4688 |             100.2461 |           0.0078 |   46.8750 |         - |   531.11 KB |
| ConcurrentSmallDatasets  |  6,086.50 μs |   283.823 μs |   827.926 μs |  6,138.08 μs |  781.2500 |             132.4766 |           9.1016 |  742.1875 |  445.3125 |  5753.78 KB |
| UpdateProduct            |     88.73 μs |     5.173 μs |    14.674 μs |     82.00 μs |    3.4180 |               2.0005 |           0.0024 |         - |         - |    14.63 KB |
