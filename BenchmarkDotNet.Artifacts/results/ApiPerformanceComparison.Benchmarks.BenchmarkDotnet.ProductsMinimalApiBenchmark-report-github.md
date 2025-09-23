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
| ColdStartSingleRequest   | 11,959.39 μs | 268.044 μs |   760.397 μs | 11,737.92 μs |  156.2500 |               7.0000 |                - |  125.0000 |         - |   903.01 KB |
| CreateProduct            |     47.43 μs |   0.964 μs |     2.623 μs |     47.45 μs |    3.4180 |               2.0000 |           0.0063 |    0.4883 |         - |    14.57 KB |
| DeleteProduct            |    110.84 μs |   4.662 μs |    12.997 μs |    105.65 μs |    5.8594 |               4.0000 |                - |         - |         - |    24.85 KB |
| GetMediumDataset         | 21,331.98 μs | 424.140 μs | 1,094.842 μs | 20,803.68 μs | 1500.0000 |             140.2188 |                - | 1437.5000 | 1000.0000 | 10347.82 KB |
| GetSingleProduct         |     39.11 μs |   2.887 μs |     7.855 μs |     39.03 μs |    2.4414 |               2.0000 |           0.0005 |         - |         - |    11.28 KB |
| GetSmallDataset          |  2,913.40 μs | 126.991 μs |   368.423 μs |  2,870.82 μs |  187.5000 |              14.9141 |                - |  117.1875 |   93.7500 |   809.06 KB |
| ConcurrentSingleRequests |    705.06 μs |  13.467 μs |    26.895 μs |    700.47 μs |  109.3750 |             100.3398 |           0.0117 |   42.9688 |         - |   531.17 KB |
| ConcurrentSmallDatasets  |  6,246.67 μs | 350.176 μs | 1,032.501 μs |  6,037.66 μs |  796.8750 |             134.3594 |           9.2109 |  531.2500 |  359.3750 |  5832.54 KB |
| UpdateProduct            |     74.45 μs |   3.600 μs |     9.916 μs |     71.75 μs |    3.4180 |               2.0000 |           0.0034 |         - |         - |    14.61 KB |
