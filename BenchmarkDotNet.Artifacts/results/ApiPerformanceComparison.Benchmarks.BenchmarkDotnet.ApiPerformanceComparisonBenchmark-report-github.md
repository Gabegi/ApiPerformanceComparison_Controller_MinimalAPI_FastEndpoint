```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error        | StdDev        | Median       | Ratio | RatioSD | Gen0      | Gen1     | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-------------:|--------------:|-------------:|------:|--------:|----------:|---------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 24,609.52 μs | 2,723.092 μs |  7,813.062 μs | 22,151.87 μs |     ? |       ? |  533.3333 | 266.6667 |  66.6667 |  3428.01 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 28,186.54 μs | 3,213.506 μs |  9,220.153 μs | 25,618.93 μs |     ? |       ? |  500.0000 | 250.0000 |  62.5000 |  2959.94 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 34,318.21 μs | 3,804.670 μs | 10,916.311 μs | 31,593.70 μs |     ? |       ? |  714.2857 | 142.8571 |        - |   5070.2 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |          |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 22,216.63 μs | 1,146.857 μs |  3,139.504 μs | 21,349.44 μs |     ? |       ? | 1000.0000 | 875.0000 | 500.0000 | 10261.48 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 27,601.40 μs | 2,988.083 μs |  8,476.694 μs | 24,012.74 μs |     ? |       ? |  875.0000 | 750.0000 | 500.0000 | 10256.04 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 53,565.82 μs | 8,843.052 μs | 25,372.377 μs | 47,821.99 μs |     ? |       ? | 1000.0000 | 777.7778 | 555.5556 | 10263.11 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |          |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     75.45 μs |     7.530 μs |     19.571 μs |     67.66 μs |  1.05 |    0.34 |    3.4180 |        - |        - |    15.09 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     32.25 μs |     2.395 μs |      6.557 μs |     31.62 μs |  0.45 |    0.13 |    2.6855 |        - |        - |    11.34 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     55.43 μs |     1.921 μs |      5.482 μs |     56.76 μs |  0.77 |    0.16 |    2.9297 |        - |        - |     12.8 KB |        0.85 |
|                                        |               |              |              |               |              |       |         |           |          |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,162.98 μs |    42.502 μs |     67.413 μs |  2,145.29 μs |     ? |       ? |  132.8125 |  62.5000 |  39.0625 |   732.41 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  2,222.94 μs |    94.543 μs |    249.064 μs |  2,164.81 μs |     ? |       ? |  125.0000 |  62.5000 |  39.0625 |   735.33 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  3,824.34 μs |   972.042 μs |  2,709.664 μs |  2,488.88 μs |     ? |       ? |  117.1875 |  54.6875 |  39.0625 |   733.93 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |          |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |  3,760.05 μs |   119.931 μs |    315.947 μs |  3,768.02 μs |     ? |       ? |  140.6250 |  62.5000 |        - |   718.78 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |  2,626.51 μs |   231.327 μs |    617.458 μs |  2,716.90 μs |     ? |       ? |  109.3750 |  46.8750 |        - |    534.5 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |  2,508.28 μs |   289.922 μs |    788.753 μs |  2,188.62 μs |     ? |       ? |   93.7500 |  78.1250 |        - |   606.17 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    | 19,359.62 μs |   486.474 μs |  1,306.880 μs | 19,063.10 μs |     ? |       ? |  333.3333 | 250.0000 | 166.6667 |  5363.44 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    | 20,083.72 μs |   554.802 μs |  1,509.380 μs | 19,907.02 μs |     ? |       ? |  272.7273 | 181.8182 | 181.8182 |  5318.82 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    | 19,673.80 μs |   583.796 μs |  1,607.944 μs | 19,738.31 μs |     ? |       ? |  272.7273 | 181.8182 | 181.8182 |   5334.2 KB |           ? |
