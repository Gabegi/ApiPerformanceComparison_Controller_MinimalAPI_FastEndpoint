```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error        | StdDev        | Median       | Ratio | RatioSD | Gen0      | Gen1      | Gen2      | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-------------:|--------------:|-------------:|------:|--------:|----------:|----------:|----------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 23,981.10 μs | 1,456.091 μs |  4,177.798 μs | 23,584.11 μs |     ? |       ? |  166.6667 |   83.3333 |         - |  1254.77 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 17,841.98 μs | 1,961.449 μs |  5,659.229 μs | 15,066.38 μs |     ? |       ? |  133.3333 |   66.6667 |         - |    871.2 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 19,213.26 μs |   555.967 μs |  1,531.294 μs | 18,865.61 μs |     ? |       ? |  437.5000 |   31.2500 |         - |  2709.56 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |           |           |             |             |
| Controller_GetMediumDataset            | MediumDataset | 25,743.81 μs | 1,150.430 μs |  3,300.800 μs | 25,307.51 μs |     ? |       ? | 1500.0000 | 1375.0000 | 1000.0000 | 10261.98 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 42,232.81 μs | 5,516.313 μs | 16,264.974 μs | 36,314.63 μs |     ? |       ? | 1428.5714 | 1285.7143 | 1000.0000 | 10431.39 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 33,865.58 μs | 6,142.902 μs | 17,919.135 μs | 21,876.67 μs |     ? |       ? | 1400.0000 | 1200.0000 | 1000.0000 | 10270.11 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |           |           |             |             |
| Controller_GetSingleProduct            | SingleRequest |     78.45 μs |     1.483 μs |      2.857 μs |     78.42 μs |  1.00 |    0.05 |    3.4180 |         - |         - |     15.2 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     32.59 μs |     0.896 μs |      2.526 μs |     32.03 μs |  0.42 |    0.04 |    2.6855 |         - |         - |    11.37 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     56.74 μs |     1.464 μs |      4.177 μs |     57.74 μs |  0.72 |    0.06 |    2.9297 |         - |         - |    12.42 KB |        0.82 |
|                                        |               |              |              |               |              |       |         |           |           |           |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  2,107.54 μs |    35.540 μs |     58.393 μs |  2,098.03 μs |     ? |       ? |  164.0625 |  101.5625 |   78.1250 |   734.28 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  | 23,264.03 μs |   408.077 μs |    400.786 μs | 23,211.67 μs |     ? |       ? | 1500.0000 | 1468.7500 | 1000.0000 |  10434.8 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  2,098.34 μs |    40.697 μs |     60.913 μs |  2,076.93 μs |     ? |       ? |  164.0625 |  101.5625 |   78.1250 |   739.69 KB |           ? |
|                                        |               |              |              |               |              |       |         |           |           |           |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |    792.36 μs |    15.691 μs |     27.065 μs |    783.44 μs |     ? |       ? |  144.5313 |   70.3125 |         - |   724.31 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    652.58 μs |    11.670 μs |     16.359 μs |    648.90 μs |     ? |       ? |  101.5625 |   46.8750 |         - |    535.5 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    687.73 μs |     9.486 μs |     15.849 μs |    682.93 μs |     ? |       ? |  125.0000 |   39.0625 |         - |   587.05 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  4,208.41 μs |    98.299 μs |    280.453 μs |  4,149.89 μs |     ? |       ? |  531.2500 |  468.7500 |  343.7500 |  5353.06 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    | 38,290.34 μs |   812.548 μs |  2,331.353 μs | 37,934.77 μs |     ? |       ? | 1500.0000 | 1333.3333 | 1333.3333 | 91922.31 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  4,015.65 μs |    85.473 μs |    241.080 μs |  3,948.76 μs |     ? |       ? |  531.2500 |  468.7500 |  343.7500 |  5315.48 KB |           ? |
