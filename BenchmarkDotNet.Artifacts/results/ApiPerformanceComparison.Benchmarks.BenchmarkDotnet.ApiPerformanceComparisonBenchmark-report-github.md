```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | Categories    | Mean         | Error      | StdDev       | Median       | Ratio | RatioSD | Gen0      | Completed Work Items | Lock Contentions | Gen1     | Gen2     | Allocated   | Alloc Ratio |
|--------------------------------------- |-------------- |-------------:|-----------:|-------------:|-------------:|------:|--------:|----------:|---------------------:|-----------------:|---------:|---------:|------------:|------------:|
| Controller_ColdStart                   | ColdStart     | 13,091.27 μs | 349.513 μs |   980.073 μs | 12,794.61 μs |     ? |       ? |  250.0000 |               7.0000 |                - |  93.7500 |        - |  1415.23 KB |           ? |
| MinimalApi_ColdStart                   | ColdStart     | 13,738.50 μs | 272.573 μs |   698.709 μs | 13,580.94 μs |     ? |       ? |  156.2500 |               7.0000 |                - |  93.7500 |        - |   928.08 KB |           ? |
| FastEndpoints_ColdStart                | ColdStart     | 26,145.42 μs | 449.288 μs |   614.990 μs | 26,121.77 μs |     ? |       ? |  562.5000 |               8.0313 |                - |  31.2500 |        - |  3569.24 KB |           ? |
|                                        |               |              |            |              |              |       |         |           |                      |                  |          |          |             |             |
| Controller_GetMediumDataset            | MediumDataset | 25,708.82 μs | 398.802 μs |   333.018 μs | 25,754.33 μs |     ? |       ? | 1031.2500 |             135.0938 |                - | 968.7500 | 562.5000 | 10266.41 KB |           ? |
| MinimalApi_GetMediumDataset            | MediumDataset | 24,213.79 μs | 858.797 μs | 2,350.944 μs | 23,336.63 μs |     ? |       ? |  666.6667 |             124.1667 |                - | 333.3333 | 166.6667 | 10256.48 KB |           ? |
| FastEndpoints_GetMediumDataset         | MediumDataset | 28,710.27 μs | 599.270 μs | 1,709.752 μs | 28,426.94 μs |     ? |       ? |  923.0769 |             131.0000 |           0.0769 | 769.2308 | 461.5385 | 10179.39 KB |           ? |
|                                        |               |              |            |              |              |       |         |           |                      |                  |          |          |             |             |
| Controller_GetSingleProduct            | SingleRequest |    100.63 μs |   1.997 μs |     4.424 μs |     99.97 μs |  1.00 |    0.06 |    3.4180 |               2.0000 |           0.0015 |        - |        - |    15.08 KB |        1.00 |
| MinimalApi_GetSingleProduct            | SingleRequest |     40.41 μs |   0.994 μs |     2.787 μs |     40.03 μs |  0.40 |    0.03 |    2.6855 |               2.0002 |           0.0034 |        - |        - |    11.28 KB |        0.75 |
| FastEndpoints_GetSingleProduct         | SingleRequest |     76.54 μs |   1.518 μs |     3.945 μs |     76.17 μs |  0.76 |    0.05 |    2.9297 |               2.0010 |           0.0049 |        - |        - |    12.82 KB |        0.85 |
|                                        |               |              |            |              |              |       |         |           |                      |                  |          |          |             |             |
| Controller_GetSmallDataset             | SmallDataset  |  3,047.99 μs |  75.170 μs |   212.018 μs |  3,014.29 μs |     ? |       ? |  148.4375 |              15.6055 |                - |  58.5938 |  42.9688 |    820.2 KB |           ? |
| MinimalApi_GetSmallDataset             | SmallDataset  |  3,190.94 μs |  63.201 μs |   110.691 μs |  3,178.72 μs |     ? |       ? |  140.6250 |              14.7422 |                - |  70.3125 |  39.0625 |   803.48 KB |           ? |
| FastEndpoints_GetSmallDataset          | SmallDataset  |  2,467.85 μs | 102.702 μs |   286.293 μs |  2,356.98 μs |     ? |       ? |  140.6250 |              15.5586 |                - |  74.2188 |  42.9688 |   811.14 KB |           ? |
|                                        |               |              |            |              |              |       |         |           |                      |                  |          |          |             |             |
| Controller_ConcurrentSingleRequests    | Throughput    |    783.13 μs |  15.607 μs |    38.866 μs |    773.04 μs |     ? |       ? |  144.5313 |             100.0195 |           0.0430 |  62.5000 |        - |   718.82 KB |           ? |
| MinimalApi_ConcurrentSingleRequests    | Throughput    |    696.43 μs |  16.370 μs |    46.704 μs |    699.15 μs |     ? |       ? |  105.4688 |             100.2227 |           0.0117 |  50.7813 |        - |   531.12 KB |           ? |
| FastEndpoints_ConcurrentSingleRequests | Throughput    |    737.91 μs |  15.917 μs |    44.893 μs |    731.78 μs |     ? |       ? |   97.6563 |             100.1641 |           0.0273 |  93.7500 |        - |   605.92 KB |           ? |
| Controller_ConcurrentSmallDatasets     | Throughput    |  6,638.86 μs | 219.796 μs |   648.074 μs |  6,618.07 μs |     ? |       ? |  656.2500 |             142.0547 |           8.3125 | 500.0000 | 265.6250 |  5816.23 KB |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Throughput    |  6,468.05 μs | 201.619 μs |   588.132 μs |  6,413.18 μs |     ? |       ? |  640.6250 |             128.7188 |           8.2344 | 484.3750 | 265.6250 |  5757.99 KB |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Throughput    |  7,182.77 μs | 183.792 μs |   539.031 μs |  7,239.36 μs |     ? |       ? |  625.0000 |             135.1094 |           8.6250 | 453.1250 | 250.0000 |  5817.12 KB |           ? |
