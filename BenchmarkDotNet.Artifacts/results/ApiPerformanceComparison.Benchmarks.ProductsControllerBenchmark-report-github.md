```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


```
| Method           | Mean         | Error      | StdDev     | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|----------------- |-------------:|-----------:|-----------:|-------------:|----------:|----------:|----------:|------------:|
| GetSingleProduct |     61.00 μs |   0.851 μs |   1.220 μs |     61.07 μs |    3.6621 |         - |         - |    15.14 KB |
| GetSmallDataset  |  2,194.31 μs |  31.207 μs |  48.586 μs |  2,190.51 μs |  109.3750 |  109.3750 |  109.3750 |   720.38 KB |
| GetMediumDataset | 18,982.05 μs | 185.216 μs | 154.664 μs | 18,960.92 μs | 1500.0000 | 1406.2500 | 1031.2500 | 10186.43 KB |
| CreateProduct    |  1,304.31 μs | 220.249 μs | 649.409 μs |  1,264.36 μs |    5.3711 |    0.9766 |         - |    21.82 KB |
| UpdateProduct    |     87.54 μs |   2.846 μs |   7.742 μs |     84.58 μs |    4.8828 |         - |         - |     21.2 KB |
| DeleteProduct    |           NA |         NA |         NA |           NA |        NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: DefaultJob
