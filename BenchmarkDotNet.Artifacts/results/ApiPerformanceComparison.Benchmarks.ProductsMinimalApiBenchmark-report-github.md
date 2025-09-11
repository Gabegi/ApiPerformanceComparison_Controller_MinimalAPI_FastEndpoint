```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


```
| Method           | Mean         | Error      | StdDev     | Median       | Gen0      | Gen1      | Gen2      | Allocated   |
|----------------- |-------------:|-----------:|-----------:|-------------:|----------:|----------:|----------:|------------:|
| GetSingleProduct |     21.70 μs |   0.432 μs |   0.892 μs |     21.60 μs |    2.6855 |         - |         - |     11.3 KB |
| GetSmallDataset  |  2,266.49 μs |  43.553 μs |  50.156 μs |  2,255.40 μs |  109.3750 |  109.3750 |  109.3750 |   729.93 KB |
| GetMediumDataset | 20,424.45 μs | 337.303 μs | 315.513 μs | 20,373.30 μs | 1500.0000 | 1437.5000 | 1031.2500 | 10189.27 KB |
| CreateProduct    |  1,419.46 μs | 258.409 μs | 761.926 μs |  1,345.91 μs |    3.4180 |    0.4883 |         - |    14.41 KB |
| UpdateProduct    |     39.55 μs |   1.566 μs |   4.417 μs |     38.33 μs |    3.4180 |         - |         - |     14.4 KB |
| DeleteProduct    |           NA |         NA |         NA |           NA |        NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: DefaultJob
