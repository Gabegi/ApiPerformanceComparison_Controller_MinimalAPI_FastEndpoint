```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error        | StdDev       | Median       | Ratio | RatioSD | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|----------:|---------------------:|-----------------:|----------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 10,630.42 μs |   408.158 μs | 1,164.498 μs | 10,226.67 μs |     ? |       ? |  265.6250 |               7.0000 |           0.0313 |   78.1250 |        - |  1413.89 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 12,664.10 μs |   557.247 μs | 1,571.726 μs | 12,307.18 μs |     ? |       ? |  156.2500 |               7.0781 |                - |  125.0000 |        - |   927.77 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 17,171.42 μs |   411.134 μs | 1,172.990 μs | 16,812.76 μs |     ? |       ? |  437.5000 |               9.0000 |                - |   31.2500 |        - |   2804.8 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 26,016.23 μs |   719.740 μs | 2,041.783 μs | 26,094.34 μs |     ? |       ? | 1156.2500 |             131.0625 |                - | 1062.5000 | 687.5000 | 10189.18 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 27,197.77 μs | 1,333.179 μs | 3,760.254 μs | 26,271.65 μs |     ? |       ? | 1062.5000 |             137.1250 |                - | 1000.0000 | 593.7500 | 10189.99 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 22,720.55 μs |   454.312 μs |   733.629 μs | 22,583.50 μs |     ? |       ? | 1062.5000 |             139.9375 |                - | 1000.0000 | 593.7500 | 10269.78 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     90.31 μs |     6.716 μs |    18.942 μs |     83.68 μs |  1.04 |    0.28 |    3.4180 |               2.0005 |           0.0005 |         - |        - |    15.09 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     31.91 μs |     1.734 μs |     4.747 μs |     31.60 μs |  0.37 |    0.08 |    2.4414 |               2.0000 |                - |         - |        - |    11.27 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     63.67 μs |     1.875 μs |     5.226 μs |     64.18 μs |  0.73 |    0.14 |    2.9297 |               2.0000 |           0.0015 |         - |        - |    12.82 KB |        0.85 |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,752.36 μs |    68.300 μs |   191.521 μs |  2,815.69 μs |     ? |       ? |  140.6250 |              15.4766 |                - |   70.3125 |  39.0625 |   814.51 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  2,592.39 μs |    80.877 μs |   229.435 μs |  2,573.41 μs |     ? |       ? |  132.8125 |              15.7422 |                - |   70.3125 |  39.0625 |   807.27 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  2,769.10 μs |    64.730 μs |   181.511 μs |  2,776.75 μs |     ? |       ? |  140.6250 |              15.4219 |                - |   70.3125 |  39.0625 |   808.88 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |  1,037.64 μs |    60.953 μs |   173.902 μs |    978.22 μs |     ? |       ? |  144.5313 |             100.0234 |           0.0117 |   66.4063 |        - |   718.76 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    757.31 μs |    27.769 μs |    79.226 μs |    720.82 μs |     ? |       ? |  113.2813 |             100.1953 |           0.0039 |   46.8750 |        - |   531.08 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    747.40 μs |    25.115 μs |    69.595 μs |    719.21 μs |     ? |       ? |  113.2813 |             100.3047 |           0.0391 |   74.2188 |        - |   606.68 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  7,830.97 μs |   197.113 μs |   574.989 μs |  7,887.92 μs |     ? |       ? |  625.0000 |             139.0000 |           8.6094 |  546.8750 | 250.0000 |  5812.14 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  6,889.51 μs |   223.538 μs |   637.767 μs |  6,758.92 μs |     ? |       ? |  562.5000 |             136.2500 |           9.2656 |  515.6250 | 234.3750 |  5678.82 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  6,884.70 μs |   136.041 μs |   363.122 μs |  6,892.56 μs |     ? |       ? |  640.6250 |             132.9688 |           9.2969 |  468.7500 | 250.0000 |   5860.7 KB |           ? |
