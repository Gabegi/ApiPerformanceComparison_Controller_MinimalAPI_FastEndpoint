```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host] : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                 | Mean | Error |
|----------------------- |-----:|------:|
| ColdStartSingleRequest |   NA |    NA |

Benchmarks with issues:
  ProductsFastEndpointsBenchmark.ColdStartSingleRequest: .NET 9.0(Runtime=.NET 9.0)
