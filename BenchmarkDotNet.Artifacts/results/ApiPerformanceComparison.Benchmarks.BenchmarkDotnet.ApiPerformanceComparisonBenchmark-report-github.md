```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error      | StdDev       | Median       | Ratio | RatioSD | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-----------:|-------------:|-------------:|------:|--------:|----------:|---------------------:|-----------------:|----------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 11,509.62 μs | 461.733 μs | 1,317.349 μs | 11,312.67 μs |     ? |       ? |  281.2500 |               7.0000 |           0.0313 |   93.7500 |        - |  1417.95 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 11,763.06 μs | 368.218 μs | 1,026.446 μs | 11,490.43 μs |     ? |       ? |  156.2500 |               7.0000 |                - |   93.7500 |        - |   930.15 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 16,500.01 μs | 389.696 μs | 1,130.580 μs | 16,184.55 μs |     ? |       ? |  437.5000 |               8.0000 |                - |   31.2500 |        - |  2796.97 KB |           ? |
|                                        |               |              |            |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 21,882.78 μs | 436.694 μs | 1,012.105 μs | 21,721.90 μs |     ? |       ? | 1062.5000 |             137.3750 |                - |  968.7500 | 593.7500 | 10217.33 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 21,558.31 μs | 423.791 μs |   893.919 μs | 21,507.80 μs |     ? |       ? | 1062.5000 |             137.4688 |                - | 1000.0000 | 593.7500 | 10330.35 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 21,861.49 μs | 412.585 μs |   536.478 μs | 21,894.89 μs |     ? |       ? | 1062.5000 |             136.7500 |                - | 1031.2500 | 593.7500 |  9921.02 KB |           ? |
|                                        |               |              |            |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     78.65 μs |   2.179 μs |     6.001 μs |     78.52 μs |  1.01 |    0.11 |    3.4180 |               2.0010 |           0.0029 |         - |        - |    15.08 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     51.98 μs |   7.968 μs |    23.494 μs |     42.88 μs |  0.66 |    0.30 |    2.6855 |               2.0000 |           0.0010 |         - |        - |    11.28 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     72.22 μs |   4.963 μs |    14.080 μs |     68.28 μs |  0.92 |    0.19 |    2.9297 |               2.0005 |           0.0020 |         - |        - |     12.8 KB |        0.85 |
|                                        |               |              |            |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,416.45 μs |  53.485 μs |   153.459 μs |  2,369.75 μs |     ? |       ? |  132.8125 |              15.0859 |                - |   70.3125 |  39.0625 |   810.65 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  2,276.05 μs |  48.819 μs |   139.283 μs |  2,269.55 μs |     ? |       ? |  148.4375 |              15.0469 |                - |   62.5000 |  39.0625 |   812.15 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  2,393.30 μs |  75.232 μs |   218.262 μs |  2,404.89 μs |     ? |       ? |  140.6250 |              15.5000 |                - |   70.3125 |  39.0625 |    812.1 KB |           ? |
|                                        |               |              |            |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |    748.92 μs |  14.953 μs |    36.113 μs |    742.40 μs |     ? |       ? |  144.5313 |             100.0820 |           0.0391 |   62.5000 |        - |   718.88 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    626.06 μs |  12.338 μs |    28.596 μs |    621.75 μs |     ? |       ? |  105.4688 |             100.2773 |           0.0234 |   50.7813 |        - |   531.21 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    703.39 μs |  15.029 μs |    42.142 μs |    688.76 μs |     ? |       ? |  121.0938 |             100.1367 |           0.0117 |   66.4063 |        - |   606.31 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  7,269.32 μs | 198.979 μs |   557.958 μs |  7,159.52 μs |     ? |       ? |  640.6250 |             130.3281 |           8.5625 |  515.6250 | 265.6250 |  5910.08 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  6,259.13 μs | 270.792 μs |   789.913 μs |  6,180.20 μs |     ? |       ? |  671.8750 |             130.0313 |           8.7031 |  421.8750 | 265.6250 |  5842.07 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  6,687.82 μs | 268.876 μs |   780.057 μs |  6,702.79 μs |     ? |       ? |  625.0000 |             118.3125 |           8.8125 |  531.2500 | 281.2500 |  5916.04 KB |           ? |
