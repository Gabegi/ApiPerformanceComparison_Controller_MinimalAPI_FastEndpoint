```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean          | Error         | StdDev        | Median       | Ratio | RatioSD | Gen0      | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |--------------:|--------------:|--------------:|-------------:|------:|--------:|----------:|----------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     |  20,127.98 μs |  2,557.494 μs |  7,213.458 μs | 16,487.56 μs |     ? |       ? |  187.5000 |   62.5000 |        - |  1217.18 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 103,218.18 μs | 11,532.964 μs | 33,459.223 μs | 86,150.72 μs |     ? |       ? | 3000.0000 | 1800.0000 | 200.0000 | 18875.23 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     |  56,110.35 μs | 11,246.084 μs | 31,903.271 μs | 50,248.60 μs |     ? |       ? |  333.3333 |         - |        - |  2678.04 KB |           ? |
|                                        |               |               |               |               |              |       |         |           |           |          |             |             |
| Controller_GetMediumDataset            | MediumDataset |  55,969.46 μs |  7,706.039 μs | 20,833.726 μs | 48,371.30 μs |     ? |       ? |  666.6667 |  333.3333 | 333.3333 | 10255.99 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset |  44,428.52 μs |  3,901.044 μs | 10,479.903 μs | 41,479.27 μs |     ? |       ? |  600.0000 |  400.0000 | 200.0000 |  10249.4 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset |  40,986.64 μs |  1,357.185 μs |  3,738.081 μs | 40,630.29 μs |     ? |       ? |  714.2857 |  571.4286 | 285.7143 | 10260.12 KB |           ? |
|                                        |               |               |               |               |              |       |         |           |           |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     112.05 μs |      7.255 μs |     19.738 μs |    112.11 μs |  1.03 |    0.26 |    3.4180 |         - |        - |     15.2 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |      62.41 μs |      3.403 μs |      9.542 μs |     62.14 μs |  0.57 |    0.14 |    2.6855 |         - |        - |    11.48 KB |        0.76 |
| FastEndpoints_GetSingleProduct         | SingleRequest |      84.59 μs |      2.742 μs |      7.460 μs |     84.49 μs |  0.78 |    0.16 |    2.9297 |         - |        - |     12.4 KB |        0.82 |
|                                        |               |               |               |               |              |       |         |           |           |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |   2,419.97 μs |    107.767 μs |    289.510 μs |  2,358.99 μs |     ? |       ? |   93.7500 |   39.0625 |  15.6250 |      736 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |   3,710.70 μs |    820.071 μs |  2,326.404 μs |  2,633.15 μs |     ? |       ? |   78.1250 |   31.2500 |  15.6250 |   738.17 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |   2,612.65 μs |    119.081 μs |    311.613 μs |  2,579.58 μs |     ? |       ? |  109.3750 |   31.2500 |  15.6250 |   732.17 KB |           ? |
|                                        |               |               |               |               |              |       |         |           |           |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |   7,756.69 μs |  1,254.750 μs |  3,699.658 μs |  6,543.52 μs |     ? |       ? |  156.2500 |   31.2500 |        - |   724.54 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |   3,803.51 μs |     71.785 μs |    176.089 μs |  3,819.16 μs |     ? |       ? |  109.3750 |   31.2500 |        - |   541.46 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |   3,728.85 μs |    141.417 μs |    367.562 μs |  3,636.05 μs |     ? |       ? |  109.3750 |   46.8750 |        - |      587 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  29,643.06 μs |  3,884.433 μs | 11,269.445 μs | 24,412.34 μs |     ? |       ? |  250.0000 |  125.0000 | 125.0000 |  5337.61 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  20,779.55 μs |    619.347 μs |  1,631.609 μs | 20,807.20 μs |     ? |       ? |  250.0000 |  125.0000 | 125.0000 |  5298.11 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  21,329.22 μs |    499.849 μs |  1,316.802 μs | 21,323.58 μs |     ? |       ? |  250.0000 |  125.0000 | 125.0000 |  5306.72 KB |           ? |
