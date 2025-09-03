```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


```
| Method             | Mean | Error |
|------------------- |-----:|------:|
| GetSingleProduct   |   NA |    NA |
| Get50kProducts     |   NA |    NA |
| Get100000kProducts |   NA |    NA |
| Get5kproducts      |   NA |    NA |

Benchmarks with issues:
  ProductsApiBenchmark.GetSingleProduct: DefaultJob
  ProductsApiBenchmark.Get50kProducts: DefaultJob
  ProductsApiBenchmark.Get100000kProducts: DefaultJob
  ProductsApiBenchmark.Get5kproducts: DefaultJob
