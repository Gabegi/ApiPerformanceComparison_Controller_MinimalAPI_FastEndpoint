```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.304
  [Host]   : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                         | Mean          | Error         | StdDev        | StdErr       | Median        | Min           | Q1            | Q3            | Max           | Op/s       | Ratio    | RatioSD | Rank | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Allocated   | Alloc Ratio |
|------------------------------- |--------------:|--------------:|--------------:|-------------:|--------------:|--------------:|--------------:|--------------:|--------------:|-----------:|---------:|--------:|-----:|----------:|---------------------:|-----------------:|----------:|------------:|------------:|
| Controller_GetSingleProduct    |      68.51 μs |      2.055 μs |      5.694 μs |     0.604 μs |      66.64 μs |      61.48 μs |      64.20 μs |      70.83 μs |      85.00 μs | 14,595.616 |     1.01 |    0.11 |    2 |    3.4180 |               2.0005 |           0.0005 |         - |    15.08 KB |        1.00 |
| Controller_Get5kProducts       |   7,297.87 μs |    145.409 μs |    362.120 μs |    42.383 μs |   7,265.45 μs |   6,359.61 μs |   7,068.12 μs |   7,455.38 μs |   8,248.44 μs |    137.026 |   107.18 |    9.69 |    3 |  242.1875 |              15.7422 |                - |  187.5000 |  1405.33 KB |       93.20 |
| Controller_Get50kProducts      |  98,775.21 μs |  6,214.302 μs | 17,729.756 μs | 1,828.685 μs |  92,877.47 μs |  76,015.70 μs |  84,567.14 μs | 110,445.47 μs | 148,873.67 μs |     10.124 | 1,450.72 |  282.03 |    6 | 2000.0000 |             114.6667 |                - | 1000.0000 | 13602.18 KB |      902.11 |
| Controller_Get100kProducts     | 182,777.54 μs | 11,123.810 μs | 29,498.769 μs | 3,257.594 μs | 173,382.35 μs | 152,233.10 μs | 166,736.83 μs | 185,107.40 μs | 299,758.80 μs |      5.471 | 2,684.47 |  477.29 |    8 | 4000.0000 |             208.0000 |                - | 2000.0000 | 27179.52 KB |    1,802.58 |
| MinimalApi_GetSingleProduct    |      48.15 μs |      4.035 μs |     11.447 μs |     1.187 μs |      48.96 μs |      23.96 μs |      41.23 μs |      54.83 μs |      74.83 μs | 20,770.382 |     0.71 |    0.18 |    1 |    2.6855 |               2.0012 |           0.0027 |         - |    11.25 KB |        0.75 |
| MinimalApi_Get5kProducts       |   7,825.01 μs |    133.547 μs |    124.920 μs |    32.254 μs |   7,786.30 μs |   7,649.51 μs |   7,725.46 μs |   7,906.33 μs |   8,077.08 μs |    127.795 |   114.93 |    8.88 |    4 |  234.3750 |              15.3125 |                - |  171.8750 |  1400.39 KB |       92.88 |
| MinimalApi_Get50kProducts      |  75,270.92 μs |  1,085.996 μs |  1,115.238 μs |   270.485 μs |  74,894.13 μs |  74,185.67 μs |  74,559.20 μs |  75,642.83 μs |  78,640.30 μs |     13.285 | 1,105.51 |   85.22 |    5 | 2000.0000 |             108.6667 |                - | 1000.0000 | 13596.62 KB |      901.74 |
| MinimalApi_Get100kProducts     | 149,225.53 μs |  2,659.050 μs |  2,076.011 μs |   599.293 μs | 149,509.40 μs | 144,904.30 μs | 148,649.09 μs | 150,242.06 μs | 152,101.25 μs |      6.701 | 2,191.69 |  168.54 |    7 | 4000.0000 |             242.5000 |                - | 2000.0000 | 27179.65 KB |    1,802.59 |
| FastEndpoints_GetSingleProduct |            NA |            NA |            NA |           NA |            NA |            NA |            NA |            NA |            NA |         NA |        ? |       ? |    ? |        NA |                   NA |               NA |        NA |          NA |           ? |
| FastEndpoints_Get5kProducts    |            NA |            NA |            NA |           NA |            NA |            NA |            NA |            NA |            NA |         NA |        ? |       ? |    ? |        NA |                   NA |               NA |        NA |          NA |           ? |
| FastEndpoints_Get50kProducts   |            NA |            NA |            NA |           NA |            NA |            NA |            NA |            NA |            NA |         NA |        ? |       ? |    ? |        NA |                   NA |               NA |        NA |          NA |           ? |
| FastEndpoints_Get100kProducts  |            NA |            NA |            NA |           NA |            NA |            NA |            NA |            NA |            NA |         NA |        ? |       ? |    ? |        NA |                   NA |               NA |        NA |          NA |           ? |

Benchmarks with issues:
  ProductsApiBenchmark.FastEndpoints_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_Get5kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_Get50kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_Get100kProducts: .NET 9.0(Runtime=.NET 9.0)
