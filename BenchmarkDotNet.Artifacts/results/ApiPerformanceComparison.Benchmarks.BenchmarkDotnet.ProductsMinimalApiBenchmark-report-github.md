```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error      | StdDev       | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|------------------------- |-------------:|-----------:|-------------:|-------------:|----------:|----------:|----------:|------------:|
| ColdStartSingleRequest   | 12,337.70 μs | 242.188 μs |   589.519 μs | 12,093.77 μs |  156.2500 |  125.0000 |         - |   819.98 KB |
| CreateProduct            |  1,738.91 μs | 288.010 μs |   849.205 μs |  1,819.37 μs |    3.4180 |    0.4883 |         - |     14.6 KB |
| DeleteProduct            |           NA |         NA |           NA |           NA |        NA |        NA |        NA |          NA |
| GetMediumDataset         | 22,476.60 μs | 717.856 μs | 2,012.945 μs | 21,829.02 μs | 1375.0000 | 1343.7500 |  937.5000 | 10197.02 KB |
| GetSingleProduct         |     30.74 μs |   0.816 μs |     2.207 μs |     30.23 μs |    2.6855 |         - |         - |    11.37 KB |
| GetSmallDataset          | 20,560.80 μs | 252.059 μs |   223.444 μs | 20,506.53 μs | 1468.7500 | 1406.2500 | 1000.0000 | 10203.85 KB |
| ConcurrentSingleRequests |    682.36 μs |  13.371 μs |    25.762 μs |    676.33 μs |  109.3750 |   50.7813 |         - |   535.57 KB |
| ConcurrentSmallDatasets  | 31,962.51 μs | 636.101 μs | 1,630.571 μs | 31,541.28 μs | 1333.3333 | 1166.6667 | 1166.6667 |  45809.7 KB |
| UpdateProduct            |     43.98 μs |   1.338 μs |     3.773 μs |     43.85 μs |    3.4180 |         - |         - |    14.66 KB |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
