    # ApiPerformanceComparison_Controller_MinimalAPI_FastEndpoint

    # Goals

    Compare Controllers vs Minimal APIs vs FastEndpoints in terms of performance only.

    Same host, same runtime, same payloads, same DB. Only the web framework surface differs.

    # Benchmarks

    What does performance mean?

    ## Micro-benchmarks

    Tool: BenchmarkDotNet

    Scenarios:

    - Simple GET requests (no parameters)
    - GET with route parameters (e.g., /api/products/{id})
    - GET with query parameters (e.g., /api/products?category=electronics&page=1)
    - POST requests with JSON payload
    - PUT/PATCH requests with validation

    Metrics:

    - Throughput (requests/second)
    - Latency (mean, median, P95, P99)
    - Memory allocation per request
    - CPU usage
    - Startup time
    - First request time (cold start)

    Scenarios:

    - Constant Load: Steady RPS over time
    - Spike Load: Sudden traffic increases
    - Ramp-up Load: Gradual increase in traffic
    - Mixed Workload: Combination of different endpoint types

    ## Load Testing Scenarios

    tool: NBomber, K6, # Bombardier Load Tests

    Scenarios:

    - Constant Load: Steady RPS over time
    - Spike Load: Sudden traffic increases
    - Ramp-up Load: Gradual increase in traffic
    - Mixed Workload: Combination of different endpoint types

    - High-frequency endpoints (called very often)
    - Data-intensive endpoints (large payloads)
    - Computation-heavy endpoints (complex business logic)

    ## .NET runtime counters & traces

    dotnet-counters: CPU %, GC (Gen0/1/2), allocation rate, exceptions.
    dotnet-counters monitor Microsoft-AspNetCore-Server-Kestrel System.Runtime

    dotnet-trace / PerfView for deeper investigation.

    Add ETW/EventPipe annotations if you need custom timings.

    ## Reporting and Analysis

    Automated Reports:

    - BenchmarkDotNet HTML reports
    - NBomber detailed reports

    ## Reducing external impact

    - Disable request logging & developer exception page. Keep middleware minimal and identical.
    - Fix Kestrel to the same limits (ThreadCount, MaxConcurrentConnections, HTTP/1.1 vs HTTP/2) across apps. Run initial tests on HTTP/1.1; add HTTP/2 later.

    ```
    builder.Logging.ClearProviders(); // keep logging out of the hot path
    builder.WebHost.UseKestrel(o =>
    {
        o.AddServerHeader = false;
        // Optionally: o.Limits.MaxConcurrentConnections = ... (but keep consistent!)
    });
    builder.Services.ConfigureHttpJsonOptions(opts =>
    {
        opts.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // same options across all apps
    });
    ```

    # Try different environments

    Please, don't extrapolate your results. Or do it very carefully. I remind you again: the results in different environments may vary significantly. If a Foo1 method is faster than a Foo2 method for CLR4, .NET Framework 4.5, x64, RyuJIT, Windows, it means that the Foo1 method is faster than the Foo2 method for CLR4, .NET Framework 4.5, x64, RyuJIT, Windows and nothing else. And you can not say anything about methods performance for CLR 2 or .NET Framework 4.6 or LegacyJIT-x64 or x86 or Linux+Mono until you try it.

    # Data

    ## Seeder

    Instead of a DbContext, we just generate a fixed list of products at startup and inject it everywhere.
    for pure API performance testing, you don’t need Bogus or EF Core’s DbContext at all.

    Why?
    Because the actual data values don’t matter for your benchmark. What matters is:

    How the framework parses requests

    Routing speed

    Serialization performance (small vs medium vs large JSON)

    Memory allocations per request

    ## Why this beats Bogus + DbContext

    No external dependency → pure .NET only.

    Much faster → Bogus generates random text, which is wasted work for API perf tests.

    Deterministic & reproducible → Product 000123 is always the same.

    Focus on API layer → not EF Core, not database.

    Lighter project → no seeding overhead, no DB engine noise.

    ## Data size
    Data Set Recommendations
    Optimal Dataset Sizes
    1. Small Dataset (1,000 products):

    Purpose: Simulates typical paginated API responses
    Use case: Standard web/mobile app pagination (50-100 items per page)
    Memory impact: ~100KB-500KB JSON payload
    Focus: Tests routing and serialization overhead

    2. Medium Dataset (10,000 products):

    Purpose: Tests serialization performance and memory pressure
    Use case: Data export, reporting, or bulk operations
    Memory impact: ~1-5MB JSON payload
    Focus: Reveals performance differences under moderate load

    3. Avoid Large Datasets (50k+ products):

    Why: Tests JSON serialization more than framework performance
    Problem: Can cause memory pressure that skews results
    Alternative: Use concurrent smaller requests instead

    # Project Structure

    ```
    ApiPerformanceComparison/
    ├── src/
    │ ├── ApiPerformanceComparison.Controllers/
    │ │ ├── Controllers/
    │ │ │ ├── ProductsController.cs
    │ │ └── Program.cs
    │ ├── ApiPerformanceComparison.MinimalApi/
    │ │ ├── Endpoints/
    │ │ │ ├── ProductEndpoints.cs
    │ │ └── Program.cs
    │ ├── ApiPerformanceComparison.FastEndpoints/
    │ │ ├── Endpoints/
    │ │ │ ├── Products/
    │ │ └── Program.cs
    │ └── ApiPerformanceComparison.Shared/
    │ ├── Models/
    │ │ ├── Product.cs
    │ ├── Infra/
    │ │ ├── ProductDbContext.cs
    │ └── Data/
    │ └── SampleDataGenerator.cs
    ├── tests/
    │ ├── ApiPerformanceComparison.Benchmarks/
    │ │ ├── ControllerBenchmarks.cs
    │ │ ├── MinimalApiBenchmarks.cs
    │ │ ├── FastEndpointsBenchmarks.cs
    │ │ └── Program.cs
    │ └── ApiPerformanceComparison.LoadTests/
    │ ├── Controllers/
    │ ├── MinimalApi/
    │ └── FastEndpoints/
    └── results/
    ├── benchmarks/
    ├── load-tests/
    └── reports/
    ```

    # Results

    ```
    | Method                      | Mean          | Error        | StdDev       | Median        | Min           | Max           | Ratio    | RatioSD | Rank | Gen0      | Completed Work Items | Lock Contentions | Gen1      | Allocated   | Alloc Ratio |
    |---------------------------- |--------------:|-------------:|-------------:|--------------:|--------------:|--------------:|---------:|--------:|-----:|----------:|---------------------:|-----------------:|----------:|------------:|------------:|
    | Controller_GetSingleProduct |      89.66 us |     8.285 us |    23.369 us |      83.21 us |      66.11 us |     156.87 us |     1.06 |    0.36 |    2 |    3.4180 |               2.0010 |           0.0015 |         - |    15.08 KB |        1.00 |
    | Controller_Get5kProducts    |   8,606.71 us |   221.200 us |   638.213 us |   8,452.29 us |   7,812.98 us |  10,382.69 us |   101.48 |   23.04 |    4 |  250.0000 |              14.1094 |           0.0156 |  171.8750 |  1405.02 KB |       93.19 |
    | Controller_Get50kProducts   |  92,747.90 us | 1,837.818 us | 2,861.262 us |  92,874.38 us |  87,737.30 us |  99,713.27 us | 1,093.54 |  236.64 |    5 | 2000.0000 |             112.6667 |                - | 1000.0000 | 13601.14 KB |      902.10 |
    | Controller_Get100kProducts  | 174,667.87 us | 3,442.675 us | 6,029.568 us | 173,945.60 us | 162,790.60 us | 192,053.70 us | 2,059.41 |  446.83 |    6 | 4000.0000 |             220.0000 |                - | 2000.0000 | 27181.12 KB |    1,802.80 |
    | MinimalApi_GetSingleProduct |      40.44 us |     3.195 us |     9.168 us |      39.79 us |      23.48 us |      66.70 us |     0.48 |    0.15 |    1 |    2.6855 |               2.0005 |           0.0029 |         - |    11.25 KB |        0.75 |
    | MinimalApi_Get5kProducts    |   7,780.80 us |   153.442 us |   143.530 us |   7,783.10 us |   7,562.13 us |   8,041.79 us |    91.74 |   19.72 |    3 |  250.0000 |              14.9766 |                - |  179.6875 |  1400.53 KB |       92.89 |
    | MinimalApi_Get50kProducts   |  95,422.82 us | 2,500.818 us | 7,012.563 us |  93,619.10 us |  81,321.60 us | 114,671.47 us | 1,125.08 |  255.17 |    5 | 2000.0000 |             117.6667 |           0.3333 | 1000.0000 | 13597.82 KB |      901.88 |
    | MinimalApi_Get100kProducts  | 172,360.34 us | 3,399.614 us | 8,145.258 us | 171,476.40 us | 155,624.10 us | 195,931.50 us | 2,032.20 |  445.96 |    6 | 4000.0000 |             274.0000 |                - | 2000.0000 | 27198.84 KB |    1,803.98 |

    ```

    ## 1

    our table compares Controller endpoints vs Minimal API endpoints, with different payload sizes (SingleProduct, 5k, 50k, 100k). The metrics in your BenchmarkDotNet table can be grouped into categories.

    1. ⏱ Latency

    Mean: Average execution time per request.

    Median: Middle value, less sensitive to outliers.

    StdDev / Error: Variability between runs.

    Min / Max: Best and worst cases observed.

    From your table:

    Minimal API is consistently faster than Controller for small payloads (SingleProduct).

    For larger payloads (5k+), both converge and differences are smaller (network/serialization dominates).

    2. 🚀 Throughput (Requests/sec)

    We can compute requests/sec as:

    Throughput=
    MeanTimeSeconds
    1
    ​

    Example:

    Controller_GetSingleProduct: 89.66 µs ≈ 0.00008966 s → ~11,160 requests/sec

    MinimalApi_GetSingleProduct: 40.44 µs ≈ 0.00004044 s → ~24,740 requests/sec

    This shows Minimal API nearly doubles throughput for small calls.

    For larger sets (100k products), throughput drops drastically (~5–6 req/sec).

    3. 📉 Latency Distribution (P95, P99)

    BenchmarkDotNet doesn’t output P95/P99 directly, but you can approximate from Mean + StdDev.
    For normally distributed results:

    P95 ≈ Mean + 2 × StdDev

    P99 ≈ Mean + 3 × StdDev

    Example:

    MinimalApi_Get5kProducts: Mean 7,780 µs, StdDev 143 µs

    P95 ≈ 7,780 + 286 = 8,066 µs

    P99 ≈ 7,780 + 429 = 8,209 µs

    That gives you an approximation of tail latency.

    4. 💾 Memory Allocation per Request

    From Allocated column:

    Controller_GetSingleProduct: 15.08 KB

    MinimalApi_GetSingleProduct: 11.25 KB

    For larger payloads (100k products), both allocate ~27 MB/request.
    → Allocation scales linearly with payload size, minimal differences between API styles.

# Why having all tests in one class?

Moving from "3 separate test classes" to "1 unified comparison class" is the biggest improvement. This ensures all frameworks are tested:

At the same time
With identical data
Under identical conditions
With direct performance ratios (1.5x slower, 2x faster, etc.)

## What 3 classes might have missed

Your FastEndpoints being 10x slower might actually be due to:

Different test execution timing
Different JIT compilation states
Different memory pressure during testing
The DI container issues we found in your code

🎯 Keep BOTH - They Serve Different PurposesYour Original Approach = "Individual App Performance"
When to use: When you want to know "How fast is my FastEndpoints app?"

Testing a single deployed application
Performance regression testing over time
Optimizing one specific framework implementation
Real-world production performance monitoring
My Proposed Approach = "Framework Comparison"
When to use: When you want to know "Which framework should I choose?"

Architecture decisions
Framework migration planning
Direct performance comparisons
Academic/research comparisons

```
📊 Recommended Structure:/Benchmarks
├── Individual/
│   ├── ControllerBenchmark.cs      (Your original approach)
│   ├── MinimalApiBenchmark.cs      (Your original approach)
│   └── FastEndpointsBenchmark.cs   (Your original approach)
└── Comparative/
    └── ApiFrameworkComparison.cs   (My proposed approach)🔧
```

     Small Improvements to Your Original Approach:Keep your individual tests, but add these small fixes
