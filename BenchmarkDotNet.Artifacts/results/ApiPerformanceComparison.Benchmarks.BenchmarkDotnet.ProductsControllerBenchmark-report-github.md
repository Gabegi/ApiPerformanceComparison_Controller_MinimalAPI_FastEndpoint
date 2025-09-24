```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-------------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 13,523.17 μs |   349.701 μs |   997.718 μs | 13,359.73 μs |  250.0000 |               7.0000 |                - |  125.0000 |         - |  1416.26 KB |
| CreateProduct            |    112.86 μs |     3.331 μs |     8.891 μs |    110.20 μs |    4.8828 |               2.0000 |                - |         - |         - |    20.59 KB |
| DeleteProduct            |    203.13 μs |    11.759 μs |    32.778 μs |    187.68 μs |    7.8125 |               4.0000 |           0.0029 |         - |         - |    34.34 KB |
| GetMediumDataset         | 28,000.06 μs | 2,592.313 μs | 7,396.016 μs | 25,131.99 μs | 1468.7500 |             138.1875 |                - | 1437.5000 | 1000.0000 | 10226.31 KB |
| GetSingleProduct         |     89.33 μs |     5.442 μs |    15.349 μs |     82.22 μs |    3.4180 |               2.0000 |                - |         - |         - |    15.08 KB |
| GetSmallDataset          |  2,658.48 μs |    44.047 μs |    36.781 μs |  2,648.44 μs |  128.9063 |              15.6172 |           0.0039 |  109.3750 |  109.3750 |   815.57 KB |
| ConcurrentSingleRequests |    761.13 μs |    14.443 μs |    29.502 μs |    751.00 μs |  144.5313 |             100.0391 |           0.0703 |   70.3125 |         - |    718.8 KB |
| ConcurrentSmallDatasets  |  5,640.52 μs |   184.103 μs |   507.073 μs |  5,492.27 μs |  859.3750 |             138.6094 |           9.0781 |  718.7500 |  468.7500 |  5764.83 KB |
| UpdateProduct            |    106.08 μs |     4.226 μs |    11.711 μs |    102.94 μs |    4.8828 |               2.0005 |                - |         - |         - |    21.36 KB |
