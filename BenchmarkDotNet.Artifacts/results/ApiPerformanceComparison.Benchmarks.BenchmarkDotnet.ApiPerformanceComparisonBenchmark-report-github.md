```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error      | StdDev     | Ratio | RatioSD | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-----------:|-----------:|------:|--------:|----------:|---------------------:|-----------------:|----------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 10,489.57 μs | 209.584 μs | 432.828 μs |     ? |       ? |  265.6250 |               7.0000 |                - |  125.0000 |        - |  1414.77 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 11,551.27 μs | 229.907 μs | 509.458 μs |     ? |       ? |  156.2500 |               7.0000 |                - |   93.7500 |        - |   928.26 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 21,922.19 μs | 429.745 μs | 494.895 μs |     ? |       ? |  562.5000 |               9.0000 |                - |   31.2500 |        - |  3571.48 KB |           ? |
|                                        |               |              |            |            |       |         |           |                      |                  |           |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 22,673.53 μs | 447.247 μs | 641.429 μs |     ? |       ? | 1062.5000 |             136.8125 |                - |  968.7500 | 593.7500 | 10218.46 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 22,528.26 μs | 399.404 μs | 354.062 μs |     ? |       ? | 1062.5000 |             137.7500 |                - | 1000.0000 | 625.0000 | 10250.75 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 22,816.24 μs | 438.576 μs | 570.273 μs |     ? |       ? | 1062.5000 |             137.5625 |                - | 1000.0000 | 593.7500 | 10331.84 KB |           ? |
|                                        |               |              |            |            |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     78.96 μs |   1.375 μs |   2.809 μs |  1.00 |    0.05 |    3.4180 |               2.0000 |                - |         - |        - |    15.09 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     27.49 μs |   0.972 μs |   2.759 μs |  0.35 |    0.04 |    2.6855 |               2.0005 |           0.0005 |         - |        - |    11.27 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     61.38 μs |   1.227 μs |   3.255 μs |  0.78 |    0.05 |    2.9297 |               2.0000 |           0.0039 |         - |        - |    12.82 KB |        0.85 |
|                                        |               |              |            |            |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,392.09 μs |  47.795 μs |  78.529 μs |     ? |       ? |  132.8125 |              15.8516 |                - |   70.3125 |  39.0625 |   810.27 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  2,268.71 μs |  45.369 μs |  65.067 μs |     ? |       ? |  132.8125 |              15.5547 |                - |   62.5000 |  39.0625 |   801.76 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  2,288.34 μs |  44.445 μs |  60.836 μs |     ? |       ? |  140.6250 |              16.3750 |                - |   70.3125 |  39.0625 |   815.41 KB |           ? |
|                                        |               |              |            |            |       |         |           |                      |                  |           |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |    774.53 μs |  15.470 μs |  27.094 μs |     ? |       ? |  144.5313 |             100.0547 |           0.0156 |   66.4063 |        - |   718.83 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    638.78 μs |  12.598 μs |  15.472 μs |     ? |       ? |  105.4688 |             100.4023 |           0.0195 |   50.7813 |        - |   531.33 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    689.38 μs |  12.022 μs |  20.086 μs |     ? |       ? |  113.2813 |             100.1563 |           0.0156 |   78.1250 |        - |   606.57 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  6,877.67 μs | 169.090 μs | 495.912 μs |     ? |       ? |  609.3750 |             120.3594 |           8.4063 |  484.3750 | 250.0000 |  5989.36 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  5,854.13 μs | 194.079 μs | 572.248 μs |     ? |       ? |  546.8750 |             125.6094 |           8.5938 |  531.2500 | 234.3750 |  5774.76 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  6,636.35 μs | 161.519 μs | 476.241 μs |     ? |       ? |  625.0000 |             141.6406 |           8.5625 |  515.6250 | 250.0000 |   5756.7 KB |           ? |
