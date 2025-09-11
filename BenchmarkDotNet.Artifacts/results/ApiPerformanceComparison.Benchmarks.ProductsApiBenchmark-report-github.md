```

BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
Intel Core i7-8650U CPU 1.90GHz (Max: 2.11GHz) (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.305
  [Host]   : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  .NET 9.0 : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                                 | InvocationCount | UnrollFactor | Categories    | Mean | Error | StdErr | StdDev | Min | Q1 | Median | Q3 | Max | Op/s | Ratio | RatioSD | Rank | Alloc Ratio |
|--------------------------------------- |---------------- |------------- |-------------- |-----:|------:|-------:|-------:|----:|---:|-------:|---:|----:|-----:|------:|--------:|-----:|------------:|
| Controller_ColdStart                   | 1               | 1            | ColdStart     |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_ColdStart                   | 1               | 1            | ColdStart     |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_ColdStart                | 1               | 1            | ColdStart     |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
|                                        |                 |              |               |      |       |        |        |     |    |        |    |     |      |       |         |      |             |
| Controller_GetMediumDataset            | Default         | 16           | MediumDataset |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_GetMediumDataset            | Default         | 16           | MediumDataset |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_GetMediumDataset         | Default         | 16           | MediumDataset |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
|                                        |                 |              |               |      |       |        |        |     |    |        |    |     |      |       |         |      |             |
| Controller_GetSingleProduct            | Default         | 16           | SingleRequest |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_GetSingleProduct            | Default         | 16           | SingleRequest |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_GetSingleProduct         | Default         | 16           | SingleRequest |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
|                                        |                 |              |               |      |       |        |        |     |    |        |    |     |      |       |         |      |             |
| Controller_GetSmallDataset             | Default         | 16           | SmallDataset  |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_GetSmallDataset             | Default         | 16           | SmallDataset  |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_GetSmallDataset          | Default         | 16           | SmallDataset  |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
|                                        |                 |              |               |      |       |        |        |     |    |        |    |     |      |       |         |      |             |
| Controller_ConcurrentSingleRequests    | Default         | 16           | Throughput    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_ConcurrentSingleRequests    | Default         | 16           | Throughput    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_ConcurrentSingleRequests | Default         | 16           | Throughput    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| Controller_ConcurrentSmallDatasets     | Default         | 16           | Throughput    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| MinimalApi_ConcurrentSmallDatasets     | Default         | 16           | Throughput    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |
| FastEndpoints_ConcurrentSmallDatasets  | Default         | 16           | Throughput    |   NA |    NA |     NA |     NA |  NA | NA |     NA | NA |  NA |   NA |     ? |       ? |    ? |           ? |

Benchmarks with issues:
  ProductsApiBenchmark.Controller_ColdStart: .NET 9.0(Runtime=.NET 9.0, InvocationCount=1, UnrollFactor=1)
  ProductsApiBenchmark.MinimalApi_ColdStart: .NET 9.0(Runtime=.NET 9.0, InvocationCount=1, UnrollFactor=1)
  ProductsApiBenchmark.FastEndpoints_ColdStart: .NET 9.0(Runtime=.NET 9.0, InvocationCount=1, UnrollFactor=1)
  ProductsApiBenchmark.Controller_GetMediumDataset: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_GetMediumDataset: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_GetMediumDataset: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_GetSingleProduct: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_GetSmallDataset: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_GetSmallDataset: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_GetSmallDataset: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_ConcurrentSingleRequests: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.Controller_ConcurrentSmallDatasets: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.MinimalApi_ConcurrentSmallDatasets: .NET 9.0(Runtime=.NET 9.0)
  ProductsApiBenchmark.FastEndpoints_ConcurrentSmallDatasets: .NET 9.0(Runtime=.NET 9.0)
