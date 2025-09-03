```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2


```
| Method             | Mean          | Error         | StdDev        | Median        | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------- |--------------:|--------------:|--------------:|--------------:|----------:|----------:|---------:|------------:|
| GetSingleProduct   |      98.55 μs |      3.206 μs |      8.937 μs |      95.32 μs |    3.4180 |         - |        - |    15.08 KB |
| Get50kProducts     | 115,085.24 μs |  4,438.637 μs | 12,591.674 μs | 113,254.97 μs | 2333.3333 | 1333.3333 | 333.3333 | 13606.19 KB |
| Get100000kProducts | 247,917.74 μs | 13,856.818 μs | 38,856.007 μs | 234,740.50 μs | 4000.0000 | 2000.0000 |        - | 27182.44 KB |
| Get5kproducts      |   7,734.30 μs |    187.217 μs |    531.103 μs |   7,595.19 μs |  234.3750 |  171.8750 |        - |  1405.49 KB |
