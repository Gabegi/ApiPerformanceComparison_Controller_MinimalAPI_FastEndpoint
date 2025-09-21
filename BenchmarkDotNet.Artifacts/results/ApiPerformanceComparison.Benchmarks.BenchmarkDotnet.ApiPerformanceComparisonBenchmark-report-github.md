```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error        | StdDev       | Median       | Ratio | RatioSD | Gen0      | Gen1     | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|----------:|---------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 21,297.13 μs |   810.736 μs | 2,273.391 μs | 20,889.80 μs |     ? |       ? |  593.7500 | 312.5000 |  93.7500 |  3390.88 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 25,945.37 μs | 1,441.022 μs | 4,180.667 μs | 24,777.58 μs |     ? |       ? |  500.0000 | 285.7143 |  71.4286 |   2971.8 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 28,574.41 μs | 1,145.928 μs | 3,136.958 μs | 27,576.74 μs |     ? |       ? |  727.2727 | 181.8182 |        - |  5041.32 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |          |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 22,740.14 μs |   362.373 μs |   302.598 μs | 22,858.40 μs |     ? |       ? |  937.5000 | 906.2500 | 500.0000 | 10173.07 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 22,470.29 μs |   443.859 μs |   528.382 μs | 22,317.71 μs |     ? |       ? |  937.5000 | 906.2500 | 500.0000 | 10171.12 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 22,391.04 μs |   443.687 μs |   650.351 μs | 22,324.83 μs |     ? |       ? | 1000.0000 | 937.5000 | 562.5000 | 10173.13 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |          |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     81.30 μs |     1.514 μs |     2.571 μs |     81.33 μs |  1.00 |    0.04 |    3.4180 |        - |        - |    15.09 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     32.24 μs |     0.931 μs |     2.640 μs |     31.57 μs |  0.40 |    0.03 |    2.6855 |        - |        - |    11.27 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     75.79 μs |     1.467 μs |     1.570 μs |     75.97 μs |  0.93 |    0.03 |    2.9297 |        - |        - |    12.82 KB |        0.85 |
|                                        |               |              |              |              |              |       |         |           |          |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,411.78 μs |    48.137 μs |    85.563 μs |  2,413.66 μs |     ? |       ? |  125.0000 |  62.5000 |  39.0625 |   729.42 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  2,372.00 μs |    47.342 μs |   108.776 μs |  2,327.67 μs |     ? |       ? |  125.0000 |  54.6875 |  39.0625 |   716.63 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  2,284.01 μs |    44.219 μs |    89.325 μs |  2,272.19 μs |     ? |       ? |  117.1875 |  54.6875 |  39.0625 |   724.63 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |          |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |    920.92 μs |    39.190 μs |   110.535 μs |    877.56 μs |     ? |       ? |  144.5313 |  62.5000 |        - |   718.78 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    844.03 μs |    28.049 μs |    80.025 μs |    847.30 μs |     ? |       ? |  105.4688 |  50.7813 |        - |   531.28 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    869.82 μs |    17.324 μs |    42.497 μs |    855.99 μs |     ? |       ? |  105.4688 |  89.8438 |        - |   606.22 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  5,031.39 μs |   159.489 μs |   449.840 μs |  4,936.07 μs |     ? |       ? |  406.2500 | 343.7500 | 234.3750 |  5271.62 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  4,707.94 μs |   217.296 μs |   626.947 μs |  4,493.42 μs |     ? |       ? |  390.6250 | 328.1250 | 234.3750 |  5213.42 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  4,961.16 μs |   221.715 μs |   636.142 μs |  4,826.72 μs |     ? |       ? |  406.2500 | 343.7500 | 234.3750 |  5237.32 KB |           ? |
