```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


```
| Method           | Mean         | Error      | StdDev       | Gen0      | Gen1      | Gen2      | Allocated   |
|----------------- |-------------:|-----------:|-------------:|----------:|----------:|----------:|------------:|
| GetSingleProduct |     50.95 μs |   1.273 μs |     3.548 μs |    3.1738 |         - |         - |    12.85 KB |
| GetSmallDataset  |  2,371.26 μs |  29.704 μs |    37.566 μs |  109.3750 |  109.3750 |  109.3750 |   734.43 KB |
| GetMediumDataset | 22,038.20 μs | 535.525 μs | 1,519.196 μs | 1468.7500 | 1375.0000 | 1000.0000 | 10265.03 KB |
| CreateProduct    |  1,009.94 μs | 171.780 μs |   506.497 μs |    3.9063 |         - |         - |     16.7 KB |
| UpdateProduct    |    117.06 μs |   6.729 μs |    19.198 μs |    3.9063 |         - |         - |    16.06 KB |
| DeleteProduct    |           NA |         NA |           NA |        NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.DeleteProduct: DefaultJob
