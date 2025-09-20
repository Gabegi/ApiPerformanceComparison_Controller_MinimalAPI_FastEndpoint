```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error         | StdDev        | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|--------------:|--------------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 37,039.93 μs |  5,416.397 μs | 15,885.355 μs | 30,496.27 μs |  833.3333 |  333.3333 |   83.3333 |  5110.95 KB |
| CreateProduct            |     71.84 μs |      2.105 μs |      5.726 μs |     70.49 μs |    3.9063 |    0.4883 |         - |    16.55 KB |
| DeleteProduct            |           NA |            NA |            NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 66,554.63 μs | 16,114.141 μs | 45,974.555 μs | 40,543.31 μs | 1444.4444 | 1333.3333 | 1000.0000 | 10186.33 KB |
| GetSingleProduct         |     61.66 μs |      7.289 μs |     19.330 μs |     56.55 μs |    2.9297 |         - |         - |    12.79 KB |
| GetSmallDataset          |  2,139.95 μs |     66.121 μs |    185.411 μs |  2,115.03 μs |  156.2500 |   93.7500 |   78.1250 |   720.24 KB |
| ConcurrentSingleRequests |  3,816.54 μs |    218.703 μs |    594.998 μs |  3,640.24 μs |  109.3750 |   62.5000 |         - |   606.52 KB |
| ConcurrentSmallDatasets  | 22,142.18 μs |    610.191 μs |  1,607.488 μs | 22,253.83 μs |  375.0000 |  250.0000 |  250.0000 |  5250.26 KB |
| UpdateProduct            |     75.46 μs |      5.025 μs |     13.840 μs |     70.01 μs |    3.9063 |         - |         - |     16.2 KB |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
