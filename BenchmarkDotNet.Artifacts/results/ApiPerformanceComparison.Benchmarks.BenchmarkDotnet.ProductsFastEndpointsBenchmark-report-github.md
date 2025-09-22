```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 18,570.65 μs | 469.437 μs | 1,361.921 μs |  468.7500 |   31.2500 |         - |  2897.12 KB |
| CreateProduct            |     93.64 μs |   2.793 μs |     7.785 μs |    3.9063 |    0.4883 |         - |    16.59 KB |
| DeleteProduct            |    131.21 μs |   2.904 μs |     7.901 μs |    6.8359 |         - |         - |    28.26 KB |
| GetMediumDataset         | 20,691.69 μs | 118.814 μs |   105.326 μs | 1468.7500 | 1437.5000 | 1000.0000 | 10268.37 KB |
| GetSingleProduct         |     42.64 μs |   0.847 μs |     1.691 μs |    2.9297 |         - |         - |    12.79 KB |
| GetSmallDataset          |  2,461.23 μs |  48.897 μs |    56.310 μs |  109.3750 |  109.3750 |  109.3750 |   729.78 KB |
| ConcurrentSingleRequests |    703.47 μs |   7.908 μs |    10.557 μs |  125.0000 |   66.4063 |         - |   606.45 KB |
| ConcurrentSmallDatasets  |  4,299.37 μs | 118.223 μs |   341.100 μs |  593.7500 |  515.6250 |  406.2500 |  5311.55 KB |
| UpdateProduct            |     77.71 μs |   1.087 μs |     1.816 μs |    3.9063 |         - |         - |     16.2 KB |
