```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.4946/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.304
  [Host]   : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method           | Mean         | Error        | StdDev        | Gen0      | Gen1      | Allocated   |
|----------------- |-------------:|-------------:|--------------:|----------:|----------:|------------:|
| GetSingleProduct |     311.7 μs |      6.14 μs |      14.60 μs |    3.4180 |         - |    15.08 KB |
| Get50kProducts   | 267,778.9 μs | 22,002.41 μs |  63,832.99 μs | 2000.0000 | 1000.0000 | 13601.98 KB |
| Get100kProducts  | 585,035.1 μs | 44,657.44 μs | 129,559.33 μs | 4000.0000 | 2000.0000 |  27186.3 KB |
| Get5kproducts    |  32,405.3 μs |  1,998.72 μs |   5,734.71 μs |  200.0000 |         - |  1404.71 KB |
