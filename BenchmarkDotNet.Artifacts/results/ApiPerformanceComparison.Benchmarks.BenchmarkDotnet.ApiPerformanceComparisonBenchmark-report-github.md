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
| Controller_ColdStart                   | ColdStart     | 11,725.37 μs |   358.991 μs |   994.763 μs | 11,320.33 μs |     ? |       ? |  250.0000 |               7.0000 |                - |  125.0000 |        - |   1414.8 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 12,107.93 μs |   277.124 μs |   749.221 μs | 12,000.55 μs |     ? |       ? |  156.2500 |               7.0000 |                - |  125.0000 |        - |   926.73 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 19,444.14 μs |   962.132 μs | 2,697.921 μs | 18,838.94 μs |     ? |       ? |  437.5000 |               9.0000 |                - |   31.2500 |        - |   2806.2 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 31,117.06 μs | 2,873.483 μs | 8,104.710 μs | 28,673.01 μs |     ? |       ? |  857.1429 |             134.8571 |                - |  571.4286 | 428.5714 | 10064.46 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 32,446.87 μs | 3,437.935 μs | 9,808.622 μs | 28,820.31 μs |     ? |       ? | 1166.6667 |             136.5000 |                - | 1000.0000 | 666.6667 | 10020.63 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 27,622.00 μs | 1,794.780 μs | 5,091.492 μs | 26,373.60 μs |     ? |       ? |  833.3333 |             138.3333 |                - |  500.0000 | 333.3333 | 10335.55 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     84.01 μs |     1.671 μs |     4.035 μs |     84.51 μs |  1.00 |    0.07 |    3.4180 |               2.0000 |           0.0010 |         - |        - |    15.09 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     34.82 μs |     1.358 μs |     3.764 μs |     34.88 μs |  0.42 |    0.05 |    2.6855 |               2.0002 |           0.0015 |         - |        - |    11.28 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     64.84 μs |     1.954 μs |     5.606 μs |     65.74 μs |  0.77 |    0.08 |    2.9297 |               2.0005 |           0.0044 |         - |        - |    12.82 KB |        0.85 |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  6,551.11 μs | 1,265.683 μs | 3,712.029 μs |  6,410.70 μs |     ? |       ? |  125.0000 |              16.3906 |           0.0156 |   62.5000 |  31.2500 |   820.45 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  4,317.91 μs | 1,014.863 μs | 2,911.831 μs |  2,979.07 μs |     ? |       ? |   83.3333 |              14.0833 |                - |         - |        - |   813.36 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  5,731.90 μs | 1,474.913 μs | 4,255.461 μs |  3,468.09 μs |     ? |       ? |  125.0000 |              16.0469 |           0.0156 |   62.5000 |  31.2500 |    819.1 KB |           ? |
|                                        |               |              |              |              |              |       |         |           |                      |                  |           |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |  7,054.60 μs | 1,115.746 μs | 3,289.804 μs |  5,684.09 μs |     ? |       ? |  156.2500 |             100.0938 |           0.0313 |   62.5000 |        - |   718.87 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |  4,474.25 μs |   152.993 μs |   411.005 μs |  4,448.17 μs |     ? |       ? |   93.7500 |             100.0313 |                - |   31.2500 |        - |   530.92 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |  5,542.69 μs |   945.714 μs | 2,682.834 μs |  4,320.07 μs |     ? |       ? |  125.0000 |             100.3438 |                - |   31.2500 |        - |   606.86 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    | 31,952.93 μs | 1,574.222 μs | 4,567.103 μs | 31,204.68 μs |     ? |       ? |  562.5000 |             141.1250 |           9.9375 |  437.5000 | 187.5000 |  5793.75 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    | 28,741.95 μs |   955.712 μs | 2,583.822 μs | 28,178.57 μs |     ? |       ? |  656.2500 |             127.0938 |           9.1875 |  500.0000 | 281.2500 |  5929.83 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    | 26,014.87 μs |   519.617 μs | 1,224.799 μs | 25,854.74 μs |     ? |       ? |  687.5000 |             128.4375 |           9.0938 |  468.7500 | 281.2500 |  5947.37 KB |           ? |
