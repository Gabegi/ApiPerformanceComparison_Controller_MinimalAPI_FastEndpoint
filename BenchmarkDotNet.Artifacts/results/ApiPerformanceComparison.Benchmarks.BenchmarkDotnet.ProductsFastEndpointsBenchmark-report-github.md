```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0     | Gen1    | Allocated  |
|------------------------- |-------------:|-----------:|-------------:|-------------:|---------:|--------:|-----------:|
| ColdStartSingleRequest   | 20,260.21 μs | 619.748 μs | 1,807.834 μs | 20,703.70 μs | 468.7500 | 31.2500 | 2894.52 KB |
| CreateProduct            |     84.41 μs |   1.660 μs |     2.773 μs |     84.00 μs |   3.9063 |  0.4883 |   16.58 KB |
| DeleteProduct            |           NA |         NA |           NA |           NA |       NA |      NA |         NA |
| GetMediumDataset         |    290.45 μs |   5.623 μs |     8.919 μs |    291.60 μs |  15.6250 |  1.9531 |   64.29 KB |
| GetSingleProduct         |           NA |         NA |           NA |           NA |       NA |      NA |         NA |
| GetSmallDataset          |    301.36 μs |   6.144 μs |    17.430 μs |    303.99 μs |  15.6250 |  1.9531 |   65.78 KB |
| ConcurrentSingleRequests |           NA |         NA |           NA |           NA |       NA |      NA |         NA |
| ConcurrentSmallDatasets  |    674.78 μs |  22.259 μs |    64.221 μs |    661.39 μs | 111.3281 | 60.5469 |  477.72 KB |
| UpdateProduct            |     84.86 μs |   2.319 μs |     6.504 μs |     82.41 μs |   3.9063 |       - |    16.2 KB |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsFastEndpointsBenchmark.GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsFastEndpointsBenchmark.ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
