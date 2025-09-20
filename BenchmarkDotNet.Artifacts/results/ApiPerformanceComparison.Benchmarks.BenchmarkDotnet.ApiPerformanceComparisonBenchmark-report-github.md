```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean          | Error         | StdDev        | Median       | Ratio | RatioSD | Gen0      | Gen1     | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |--------------:|--------------:|--------------:|-------------:|------:|--------:|----------:|---------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 103,697.36 μs | 23,721.882 μs | 69,944.509 μs | 97,431.56 μs |     ? |       ? |  500.0000 | 375.0000 |        - |  3393.25 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     |  31,122.55 μs |  4,252.359 μs | 12,200.816 μs | 24,640.53 μs |     ? |       ? |  500.0000 | 312.5000 |  62.5000 |   2962.9 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     |  41,532.36 μs |  5,578.241 μs | 16,183.490 μs | 37,202.84 μs |     ? |       ? |  700.0000 | 200.0000 |        - |  5031.35 KB |           ? |
|                                        |               |               |               |               |              |       |         |           |          |          |             |             |
| Controller_GetMediumDataset            | MediumDataset |  35,850.58 μs |  1,429.403 μs |  3,839.999 μs | 35,301.31 μs |     ? |       ? |  857.1429 | 714.2857 | 428.5714 | 10187.16 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset |  36,824.64 μs |  1,280.007 μs |  3,460.573 μs | 36,441.36 μs |     ? |       ? | 1000.0000 | 857.1429 | 571.4286 | 10173.47 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset |  36,314.75 μs |  1,334.834 μs |  3,654.086 μs | 35,868.84 μs |     ? |       ? |  857.1429 | 714.2857 | 428.5714 | 10185.65 KB |           ? |
|                                        |               |               |               |               |              |       |         |           |          |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |     380.77 μs |    108.353 μs |    309.137 μs |    246.48 μs |  2.01 |    2.54 |         - |        - |        - |    15.09 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |      53.52 μs |      2.967 μs |      8.171 μs |     52.97 μs |  0.28 |    0.22 |    2.6855 |        - |        - |    11.34 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     157.08 μs |     29.198 μs |     86.091 μs |    127.35 μs |  0.83 |    0.84 |    1.9531 |        - |        - |    12.79 KB |        0.85 |
|                                        |               |               |               |               |              |       |         |           |          |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |   2,462.48 μs |     70.499 μs |    196.524 μs |  2,455.04 μs |     ? |       ? |  125.0000 |  62.5000 |  39.0625 |   731.77 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |   2,599.97 μs |     68.609 μs |    186.656 μs |  2,595.23 μs |     ? |       ? |  125.0000 |  62.5000 |  39.0625 |   727.37 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |   2,723.07 μs |     58.203 μs |    163.206 μs |  2,709.59 μs |     ? |       ? |  125.0000 |  62.5000 |  39.0625 |   722.35 KB |           ? |
|                                        |               |               |               |               |              |       |         |           |          |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |   7,433.69 μs |  1,311.576 μs |  3,867.212 μs |  5,852.04 μs |     ? |       ? |  125.0000 |  31.2500 |        - |   719.01 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |   4,422.60 μs |    349.959 μs |    963.890 μs |  3,941.69 μs |     ? |       ? |  109.3750 |  46.8750 |        - |   534.58 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |   3,855.21 μs |    116.317 μs |    306.425 μs |  3,823.24 μs |     ? |       ? |  109.3750 |  62.5000 |        - |   606.17 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  22,551.70 μs |    556.571 μs |  1,436.687 μs | 22,641.08 μs |     ? |       ? |  375.0000 | 250.0000 | 250.0000 |  5249.26 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  22,386.92 μs |    762.831 μs |  2,036.149 μs | 22,135.84 μs |     ? |       ? |  375.0000 | 250.0000 | 250.0000 |  5216.76 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  21,507.37 μs |    629.707 μs |  1,658.901 μs | 21,429.51 μs |     ? |       ? |  375.0000 | 250.0000 | 250.0000 |  5236.19 KB |           ? |
