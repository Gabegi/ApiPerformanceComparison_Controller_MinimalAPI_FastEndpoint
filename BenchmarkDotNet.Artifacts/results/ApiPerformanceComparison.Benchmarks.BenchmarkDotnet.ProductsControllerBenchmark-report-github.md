```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean        | Error       | StdDev      | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |------------:|------------:|------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 11,786.1 μs |   380.24 μs | 1,090.99 μs | 11,563.20 μs |  265.6250 |               7.0000 |                - |  109.3750 |   15.6250 |  1416.36 KB |
| CreateProduct            |    103.0 μs |     2.77 μs |     7.34 μs |    101.82 μs |    4.8828 |               2.0000 |                - |    0.4883 |         - |    20.59 KB |
| DeleteProduct            |    166.2 μs |     5.88 μs |    16.21 μs |    161.96 μs |    7.8125 |               4.0000 |                - |         - |         - |    34.35 KB |
| GetMediumDataset         | 24,123.4 μs | 1,084.21 μs | 3,075.73 μs | 22,767.75 μs | 1468.7500 |             135.0938 |                - | 1437.5000 | 1000.0000 | 10285.24 KB |
| GetSingleProduct         |    100.5 μs |     1.88 μs |     4.02 μs |     99.55 μs |    3.4180 |               1.9995 |           0.0010 |         - |         - |    15.16 KB |
| GetSmallDataset          |  3,284.1 μs |   202.83 μs |   594.86 μs |  3,238.61 μs |  195.3125 |              14.7891 |                - |   93.7500 |   93.7500 |   810.72 KB |
| ConcurrentSingleRequests |    887.7 μs |    19.29 μs |    53.13 μs |    890.92 μs |  140.6250 |             100.0234 |           0.0391 |   70.3125 |         - |   718.79 KB |
| ConcurrentSmallDatasets  |  6,282.1 μs |   145.33 μs |   416.96 μs |  6,236.41 μs |  773.4375 |             123.9375 |           9.2188 |  640.6250 |  406.2500 |  5957.03 KB |
| UpdateProduct            |    117.3 μs |     3.03 μs |     8.04 μs |    116.91 μs |    4.8828 |               2.0000 |                - |         - |         - |    21.36 KB |
