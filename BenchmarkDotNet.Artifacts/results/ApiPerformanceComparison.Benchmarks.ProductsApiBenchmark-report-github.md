```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                         | Mean | Error | StdErr | StdDev | Min | Q1 | Median | Q3 | Max | Op/s | Ratio | RatioSD | Rank | Alloc Ratio |
|------------------------------- |-----:|------:|-------:|-------:|----:|---:|-------:|---:|----:|-----:|------:|--------:|-----:|------------:|
| Controller_GetSingleProduct    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| Controller_Get5kProducts       |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| Controller_Get50kProducts      |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| Controller_Get100kProducts     |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_GetSingleProduct    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_Get5kProducts       |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_Get50kProducts      |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_Get100kProducts     |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_GetSingleProduct |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_Get5kProducts    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_Get50kProducts   |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_Get100kProducts  |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |

Benchmarks with issues:
  ProductsApiBenchmark.Controller_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_Get5kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_Get50kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_Get100kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_Get5kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_Get50kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_Get100kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_Get5kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_Get50kProducts: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_Get100kProducts: .NET 9.0(Runtime=.NET 9.0)
