```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


```
| Method           | Mean         | Error        | StdDev        | Median       | Gen0      | Gen1      | Gen2      | Allocated |
|----------------- |-------------:|-------------:|--------------:|-------------:|----------:|----------:|----------:|----------:|
| GetSingleProduct |     314.8 μs |    100.92 μs |     295.98 μs |     105.0 μs |    3.4180 |         - |         - |  15.14 KB |
| GetSmallDataset  |   6,778.8 μs |  2,309.97 μs |   6,627.73 μs |   2,755.3 μs |  109.3750 |  109.3750 |  109.3750 | 736.45 KB |
| GetMediumDataset | 180,739.9 μs | 44,085.98 μs | 125,779.78 μs | 137,376.2 μs | 2000.0000 | 2000.0000 | 2000.0000 |  10177 KB |
| CreateProduct    |   1,244.5 μs |    247.77 μs |     661.34 μs |   1,368.8 μs |    4.8828 |         - |         - |  21.82 KB |
| UpdateProduct    |     159.0 μs |     20.80 μs |      56.94 μs |     141.2 μs |    4.8828 |         - |         - |   21.2 KB |
| DeleteProduct    |           NA |           NA |            NA |           NA |        NA |        NA |        NA |        NA |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: DefaultJob
