```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error        | StdDev        | Median       | Ratio | RatioSD | Gen0      | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-------------:|--------------:|-------------:|------:|--------:|----------:|----------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 12,726.98 μs |   382.361 μs |  1,072.182 μs | 12,423.09 μs |     ? |       ? |  281.2500 |  125.0000 |        - |   1405.3 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 18,283.06 μs | 1,037.002 μs |  2,941.804 μs | 17,272.18 μs |     ? |       ? |  156.2500 |   93.7500 |        - |   995.12 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 24,880.77 μs | 2,388.095 μs |  6,813.370 μs | 23,043.92 μs |     ? |       ? |  531.2500 |   31.2500 |        - |  2877.31 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |           |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 24,096.74 μs |   766.957 μs |  2,163.216 μs | 23,424.81 μs |     ? |       ? | 1312.5000 | 1218.7500 | 875.0000 |  10187.7 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 26,791.74 μs | 1,017.749 μs |  2,936.440 μs | 26,454.75 μs |     ? |       ? | 1312.5000 | 1218.7500 | 843.7500 | 10184.46 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 36,557.93 μs | 5,345.580 μs | 15,761.565 μs | 28,393.32 μs |     ? |       ? | 1142.8571 |  857.1429 | 714.2857 | 10331.79 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |           |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |    112.83 μs |     9.568 μs |     26.353 μs |    107.12 μs |  1.05 |    0.33 |    3.4180 |         - |        - |    15.08 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     41.87 μs |     2.314 μs |      6.296 μs |     40.69 μs |  0.39 |    0.10 |    2.4414 |         - |        - |    11.28 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     67.73 μs |     2.029 μs |      5.655 μs |     66.71 μs |  0.63 |    0.14 |    2.9297 |         - |        - |    12.81 KB |        0.85 |
|                                        |               |              |              |               |              |       |         |           |           |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,456.56 μs |   116.911 μs |    333.554 μs |  2,311.43 μs |     ? |       ? |  164.0625 |  101.5625 |  78.1250 |   734.69 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  3,351.84 μs |   165.841 μs |    470.463 μs |  3,303.48 μs |     ? |       ? |  187.5000 |  101.5625 |  78.1250 |   822.03 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  3,325.25 μs |   197.751 μs |    573.712 μs |  3,354.83 μs |     ? |       ? |  187.5000 |  109.3750 |  78.1250 |   822.12 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |           |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |    883.27 μs |    36.602 μs |     99.578 μs |    872.50 μs |     ? |       ? |  140.6250 |   62.5000 |        - |   718.81 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    739.11 μs |    23.279 μs |     63.727 μs |    727.08 μs |     ? |       ? |  101.5625 |   50.7813 |        - |   531.19 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    936.15 μs |    76.894 μs |    220.622 μs |    836.29 μs |     ? |       ? |  109.3750 |   82.0313 |        - |    606.1 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  5,183.28 μs |   307.560 μs |    902.020 μs |  4,837.56 μs |     ? |       ? |  507.8125 |  460.9375 | 335.9375 |  5267.81 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  7,452.51 μs |   442.568 μs |  1,297.975 μs |  7,305.94 μs |     ? |       ? |  734.3750 |  562.5000 | 359.3750 |  5778.33 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  6,322.68 μs |   342.444 μs |    977.011 μs |  5,939.45 μs |     ? |       ? |  734.3750 |  515.6250 | 328.1250 |  5738.15 KB |           ? |
