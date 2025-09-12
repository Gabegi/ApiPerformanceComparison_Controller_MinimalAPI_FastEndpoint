```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


```
| Method           | Mean        | Error       | StdDev      | Median      | Gen0      | Gen1      | Gen2      | Allocated   |
|----------------- |------------:|------------:|------------:|------------:|----------:|----------:|----------:|------------:|
| GetSingleProduct |    118.1 μs |    19.27 μs |    54.34 μs |    100.6 μs |    2.4414 |         - |         - |    11.31 KB |
| GetSmallDataset  |  3,285.3 μs |   225.33 μs |   620.62 μs |  3,134.6 μs |  109.3750 |  109.3750 |  109.3750 |    719.3 KB |
| GetMediumDataset | 47,093.3 μs | 2,430.63 μs | 6,653.82 μs | 46,492.5 μs | 1666.6667 | 1666.6667 | 1166.6667 | 10190.24 KB |
| CreateProduct    |  1,901.2 μs |   333.58 μs |   973.07 μs |  1,873.3 μs |    3.4180 |    0.4883 |         - |    14.41 KB |
| UpdateProduct    |    149.1 μs |    16.07 μs |    42.90 μs |    138.3 μs |    2.9297 |         - |         - |    14.52 KB |
| DeleteProduct    |          NA |          NA |          NA |          NA |        NA |        NA |        NA |          NA |

Benchmarks with issues:
  ProductsMinimalApiBenchmark.DeleteProduct: DefaultJob
