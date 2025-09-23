    # ApiPerformanceComparison_Controller_MinimalAPI_FastEndpoint

    # Goals

    Compare Controllers vs Minimal APIs vs FastEndpoints in terms of performance only.

    Same host, same runtime, same payloads, same DB. Only the web framework surface differs.

🚀 Benchmarks
What does performance mean?

In the context of APIs, performance measures how efficiently an API handles requests under different conditions. It’s not only about raw speed, but also about resource usage and scalability.

🧪 Micro-benchmarks

Tool: BenchmarkDotNet

All tests were run on a controlled environment with consistent hardware/software setup.

📌 Scenarios

We tested common API operations across different frameworks (Controllers, Minimal APIs, FastEndpoints):

Simple GET request – returning a small response with no parameters.

GET with route parameters – e.g. /api/products/{id}.

GET with query parameters – e.g. /api/products?category=electronics&page=1.

POST request with JSON payload – inserting/processing data.

PUT/PATCH with validation – updating data with input validation.

📊 Metrics Collected

For each scenario, we measured:

Throughput – requests per second (higher = better).

Latency – response time distributions: mean, median, P95, P99 (lower = better).

Memory allocations – bytes allocated per request.

CPU usage – efficiency under load.

Startup time – how quickly the API can start serving requests.

Cold start latency – time to process the very first request after startup.

✅ Why this matters

These benchmarks help compare frameworks not only for raw response speed, but also for efficiency and scalability. A framework that is slightly slower but uses less memory or scales better under concurrency may be a better fit depending on workload.

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

# Async

We don't have any async operations so we removed the async/await

# List vs dictionary for seed data

1️⃣ Dictionary (Dictionary<int, Product>)

Pros:

O(1) lookups by ID → GET /products/{id}, PUT, DELETE are very fast, even with 10k+ items.

Deleting and updating by key is also O(1).

Thread-safe reads if you only read (though writes still need attention in multi-threaded scenarios).

Cons:

Uses more memory than a List because it stores internal buckets for hashing.

Iteration order is not guaranteed (though Values.Take(count) works fine).

Use case: Perfect for APIs where you often look up by id or modify items by id. This is exactly your scenario.

2️⃣ List (List<Product>)

Pros:

Simpler memory model, less overhead.

Iteration is straightforward and ordered.

Cons:

Lookup by ID is O(n) → GET /products/{id} requires a linear search.

Delete and update by ID also O(n) because you have to find the index first.

For small datasets (<1k items), this is okay; for large datasets, it will slow down significantly under concurrent requests.

Use case: Fine if you mostly return all products or small subsets, and don’t often need id-based operations.

✅ Recommendation for your API

Since your API exposes:

GET /products/{id}

PUT /products/{id}

DELETE /products/{id}

…and you have 10k+ products, you definitely want Dictionary<int, Product>. Otherwise every single request by ID will loop through thousands of items — not great for performance.

# Performance issues

Here’s the catch:

System.Text.Json doesn’t stream IEnumerable<T> by default — it buffers the entire enumeration into a List<T> before serializing.

# seeding
That means: don’t seed inside the API project, only register empty singletons that the benchmark harness can override with different datasets.

# 🖥️ Test Environment

CPU: Intel i7-8650U (Kaby Lake R, 4c/8t, 1.9–2.1GHz)

Runtime: .NET 9.0.9, RyuJIT AVX2

OS: Windows 11 (24H2)

So this is a laptop-class CPU, not a high-throughput server CPU, which makes latency/GC overhead more visible.

# Tolist()
Great question 👌 — this comes down to how ASP.NET Core serializes responses and what happens when you give it a lazy IEnumerable<T> instead of a materialized collection like List<T>.

🔍 What happens without .ToList()

When you do:

return Results.Ok(products.Values.Take(count));


or in FastEndpoints:

await SendOkAsync(products.Values.Take(count));


You are giving the framework a lazy iterator (System.Linq.Enumerable+TakeIterator):

It doesn’t actually contain the products.

It only knows how to fetch them, one at a time, when enumerated.

Now the JSON serializer (System.Text.Json) comes in:

It starts iterating through the IEnumerable<Product>.

Each call yields one product.

It serializes each product immediately.

Sounds fine, but…

👉 Each enumeration step involves iterator state machines, virtual calls, and concurrency checks (ConcurrentDictionary.Values). That adds lots of overhead per item.

👉 For small datasets (10–100 items), you barely notice.
👉 For medium/large datasets (1k–10k+ items), you suddenly pay a huge performance tax (extra allocations, slower serialization).

✅ What happens with .ToList()

When you do:

var result = products.Values.Take(count).ToList();
return Results.Ok(result);


You force immediate evaluation of the query:

All count products are copied into a List<Product> (one memory allocation + bulk copy).

The JSON serializer now sees a simple List<Product> with a backing array.

It can index directly into the array, iterate very efficiently, and serialize faster.

👉 This removes all iterator overhead, all lazy-enumeration machinery, and concurrency checks.

⚡ Performance difference

In real benchmarks:

Lazy IEnumerable serialization can be 5–15× slower for 10k+ elements.

List<T> serialization is essentially optimal — one allocation, sequential memory access, cache-friendly.

That’s why in Controller APIs, you almost always see people return ToList(), and why your Minimal API & FastEndpoints were lagging: they were feeding the serializer lazy iterators.

📝 Rule of thumb

Return List<T> or Array for datasets (anything more than 1 item).

Return the raw object (Product) for single lookups.

Only use lazy IEnumerable<T> if:

You’re streaming results (e.g., with IAsyncEnumerable<T> and yield return), or

The consumer explicitly expects deferred execution.

For benchmarking your APIs fairly → always materialize with .ToList() so you’re measuring framework throughput, not the quirks of System.Text.Json + lazy iterators.

# 3. Serialization cost with large payloads

Minimal API has no automatic [ApiController] optimizations (like ProblemDetails or smart JSON options). Out of the box:

Controller templates enable System.Text.Json source generators in .NET 8/9.

Minimal API doesn’t, unless you configure it.

✅ Fix: Add optimized JSON settings:

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

# 4. Pipeline differences

Minimal API doesn’t add filters, formatters, or MVC abstractions — which is why you’d expect it to be faster.

But in real-world benchmarks, the cost of serialization + data structure access dominates, making Minimal API look worse if those aren’t optimized.

# [ApiController] Optimizations
The [ApiController] attribute in ASP.NET Core provides several automatic behaviors and conventions that streamline API development. These include:
Automatic ProblemDetails: When an error occurs (e.g., validation failure, unhandled exception), [ApiController] automatically formats the error response using the ProblemDetails specification (RFC 7807), providing a standardized and machine-readable error format.
Smart JSON Options: It automatically applies certain JSON serialization options, such as camel-casing property names in responses, which is a common convention for RESTful APIs.
Automatic Model Validation: It automatically performs model validation on incoming request bodies and query parameters, returning a 400 Bad Request with validation errors in the ProblemDetails format if validation fails.
Binding Source Inference: It infers the binding source of parameters (e.g., [FromBody], [FromQuery], [FromRoute]) based on their type and position.
Minimal API and Lack of Automatic Optimizations: Minimal APIs, by design, prioritize simplicity and a lightweight approach. They do not automatically include these [ApiController] optimizations.
Manual ProblemDetails: If you want ProblemDetails responses in a Minimal API, you need to explicitly implement them (e.g., by using Results.Problem() or creating custom middleware).
Manual JSON Configuration: JSON serialization options need to be configured explicitly in your Program.cs file (e.g., by configuring JsonSerializerOptions).
Manual Validation: Model validation needs to be implemented manually within your endpoint handlers.
In essence:
While Minimal APIs offer a faster and more lightweight way to create endpoints, they require more explicit configuration and manual implementation for features that are automatically provided by [ApiController] in traditional Controller-based APIs. This is a trade-off between simplicity/performance and built-in convenience features.


# 3️⃣ Summary Table of ToList() impact (Minimal API)
Dataset Type	Streaming (Values.Take)	With ToList()	Notes
Single Product	32–65 μs, 11–12 KB	~same	small overhead
Small Dataset	2,200 μs, 724 KB	23,000–33,000 μs, 10,200 KB	big slowdown, high allocations
Medium Dataset	32,000 μs, 10,200 KB	42,000 μs, 10,400 KB	noticeable slowdown
Concurrent Small	2,944 μs, 1.7 MB	44,693 μs, 45 MB	huge cost in concurrency

✅ Key takeaway:

Avoid ToList() in Minimal APIs for datasets larger than a few items.

Streaming (IEnumerable + Take) keeps memory allocations low and concurrency fast.

For single items, ToList() is negligible.

# ToList()
That .ToList() call copies all the count items into a new List<Product> before serialization.

For small or medium datasets (1,000 – 10,000 items), this creates huge allocations and slows down serialization.

Controllers and FastEndpoints don’t pay the same penalty because the serializer can stream directly from the IEnumerable.