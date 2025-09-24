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
| ColdStartSingleRequest   | 13,576.84 μs | 805.737 μs | 2,363.089 μs | 13,625.28 μs |  156.2500 |               7.0000 |                - |   93.7500 |         - |   903.99 KB |
| CreateProduct            |     41.84 μs |   1.568 μs |     4.267 μs |     41.90 μs |    3.4180 |               2.0000 |           0.0010 |    0.4883 |         - |    14.58 KB |
| DeleteProduct            |    103.18 μs |   1.999 μs |     4.260 μs |    102.06 μs |    5.8594 |               4.0000 |                - |         - |         - |    24.84 KB |
| GetMediumDataset         | 21,362.19 μs | 385.554 μs |   665.062 μs | 21,100.36 μs | 1468.7500 |             139.3438 |                - | 1406.2500 | 1000.0000 | 10195.34 KB |
| GetSingleProduct         |     26.08 μs |   0.551 μs |     1.535 μs |     25.63 μs |    2.6855 |               2.0002 |           0.0012 |         - |         - |    11.27 KB |
| GetSmallDataset          |  2,551.15 μs |  47.897 μs |    71.690 μs |  2,559.78 μs |  117.1875 |              14.6016 |                - |  109.3750 |  109.3750 |    803.3 KB |
| ConcurrentSingleRequests |    626.85 μs |  11.417 μs |    19.075 μs |    621.79 μs |  109.3750 |             100.2969 |           0.0313 |   54.6875 |         - |   531.19 KB |
| ConcurrentSmallDatasets  |  5,264.60 μs | 101.790 μs |   155.444 μs |  5,279.65 μs |  687.5000 |             125.8438 |           8.9063 |  632.8125 |  351.5625 |  5765.72 KB |
| UpdateProduct            |     40.15 μs |   0.889 μs |     2.537 μs |     39.80 μs |    3.4180 |               2.0000 |           0.0024 |         - |         - |    14.58 KB |
