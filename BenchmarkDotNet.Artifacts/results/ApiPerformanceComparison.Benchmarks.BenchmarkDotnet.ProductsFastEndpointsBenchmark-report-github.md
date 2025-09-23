```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|---------------------:|-----------------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 18,534.46 μs | 922.624 μs | 2,525.668 μs | 17,183.28 μs |  500.0000 |               8.0000 |                - |   83.3333 |         - |  2949.28 KB |
| CreateProduct            |     93.87 μs |  11.860 μs |    34.027 μs |     74.18 μs |    3.9063 |               2.0000 |                - |    0.4883 |         - |    16.59 KB |
| DeleteProduct            |    139.89 μs |   7.674 μs |    22.141 μs |    134.16 μs |    6.8359 |               4.0005 |           0.0015 |         - |         - |    28.26 KB |
| GetMediumDataset         | 19,146.36 μs | 379.822 μs |   713.399 μs | 19,216.34 μs | 1500.0000 |             140.4688 |                - | 1406.2500 | 1000.0000 | 10222.39 KB |
| GetSingleProduct         |     43.50 μs |   1.668 μs |     4.594 μs |     42.88 μs |    2.9297 |               2.0005 |           0.0059 |         - |         - |     12.8 KB |
| GetSmallDataset          |  2,561.30 μs | 164.514 μs |   458.601 μs |  2,440.04 μs |  195.3125 |              14.9688 |                - |  109.3750 |   93.7500 |   806.91 KB |
| ConcurrentSingleRequests |    630.11 μs |  12.559 μs |    28.856 μs |    619.75 μs |  105.4688 |             100.3555 |           0.0273 |   89.8438 |         - |   606.41 KB |
| ConcurrentSmallDatasets  |  4,426.42 μs | 121.799 μs |   325.105 μs |  4,360.56 μs |  843.7500 |             140.3594 |           9.1094 |  609.3750 |  437.5000 |  5800.98 KB |
| UpdateProduct            |     69.76 μs |   1.298 μs |     3.208 μs |     68.41 μs |    3.9063 |               2.0005 |                - |         - |         - |     16.2 KB |
