```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                   | Mean         | Error        | StdDev      | Median       | Gen0      | Gen1      | Gen2     | Allocated   |
|------------------------- |-------------:|-------------:|------------:|-------------:|----------:|----------:|---------:|------------:|
| ColdStartSingleRequest   | 30,713.15 μs | 3,146.050 μs | 8,717.69 μs | 28,402.85 μs |  500.0000 |  500.0000 |        - |  3459.77 KB |
| CreateProduct            |    229.62 μs |    54.693 μs |   158.67 μs |    183.13 μs |    3.9063 |         - |        - |    20.56 KB |
| DeleteProduct            |           NA |           NA |          NA |           NA |        NA |        NA |       NA |          NA |
| GetMediumDataset         | 29,776.15 μs | 1,880.960 μs | 5,180.71 μs | 29,537.79 μs | 1333.3333 | 1222.2222 | 888.8889 | 10266.13 KB |
| GetSingleProduct         |    115.63 μs |    21.934 μs |    62.58 μs |     82.79 μs |    3.4180 |         - |        - |    15.09 KB |
| GetSmallDataset          |  5,974.39 μs | 1,029.095 μs | 2,936.07 μs |  5,495.42 μs |  181.8182 |   90.9091 |  90.9091 |   749.42 KB |
| ConcurrentSingleRequests |  2,905.71 μs |   399.714 μs | 1,107.61 μs |  2,422.59 μs |  140.6250 |   78.1250 |        - |   719.06 KB |
| ConcurrentSmallDatasets  | 13,605.90 μs |   657.383 μs | 1,821.61 μs | 13,373.08 μs |  200.0000 |  133.3333 |  66.6667 |  5367.01 KB |
| UpdateProduct            |     83.42 μs |     4.534 μs |    12.41 μs |     78.96 μs |    4.8828 |         - |        - |    21.36 KB |

Benchmarks with issues:
  ProductsControllerBenchmark.DeleteProduct: .NET 9.0(Runtime=.NET 9.0)
