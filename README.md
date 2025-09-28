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

Dataset Type Streaming (Values.Take) With ToList() Notes
Single Product 32–65 μs, 11–12 KB ~same small overhead
Small Dataset 2,200 μs, 724 KB 23,000–33,000 μs, 10,200 KB big slowdown, high allocations
Medium Dataset 32,000 μs, 10,200 KB 42,000 μs, 10,400 KB noticeable slowdown
Concurrent Small 2,944 μs, 1.7 MB 44,693 μs, 45 MB huge cost in concurrency

✅ Key takeaway:

Avoid ToList() in Minimal APIs for datasets larger than a few items.

Streaming (IEnumerable + Take) keeps memory allocations low and concurrency fast.

For single items, ToList() is negligible.

# ToList()

That .ToList() call copies all the count items into a new List<Product> before serialization.

For small or medium datasets (1,000 – 10,000 items), this creates huge allocations and slows down serialization.

Controllers and FastEndpoints don’t pay the same penalty because the serializer can stream directly from the IEnumerable.

# Real Result number 1: Running the 3 benchmarks at the same time

📊 Benchmark Name

“API Frameworks Performance Comparison: CRUD, Dataset Serialization, and Concurrency”

📝 Description

This benchmark measures and compares the runtime performance characteristics of three popular .NET 9 web frameworks:

ASP.NET Core Controllers (MVC style)

Minimal APIs

FastEndpoints

across different workload types:

Cold Start Latency
Measures the one-time cost of handling the first request after app startup.
→ Minimal APIs have the thinnest pipeline, so they start fastest.
→ Controllers are slower due to attribute scanning & middleware.

Single CRUD Request Latency
Tests lightweight operations (e.g., fetching a single product by ID).
→ Minimal APIs consistently show ~2× better response times compared to controllers.
→ FastEndpoints shows higher per-request overhead but consistent behavior.

Dataset Serialization (Small & Medium Collections)
Evaluates performance when returning larger JSON payloads.
→ Controllers slightly outperform Minimal APIs and FastEndpoints due to built-in serializer optimizations and source-generated System.Text.Json.
→ Overhead of Minimal APIs shows here.

Concurrent Throughput
Simulates high load with many parallel requests.
→ Minimal APIs and FastEndpoints scale better, with lower latency per request.
→ Controllers show higher contention and allocations under concurrency.

Memory Allocation & GC Pressure
Each method tracks Gen0 allocations and object sizes per request.
→ Minimal APIs allocate less overall.
→ Controllers consume more memory but amortize some costs in dataset serialization.
→ FastEndpoints falls between them.

🏆 Takeaways

Minimal APIs = best for lightweight CRUD and high concurrency.

Controllers = best for dataset-heavy responses (thanks to serializer tuning).

FastEndpoints = structured like Controllers, closer to Minimal APIs in throughput, but with some extra per-request cost.

⚡ So in short:
This benchmark shows the trade-off between simplicity, serialization optimizations, and structured endpoint features in .NET 9 web frameworks.

📊 1. Cold Start (first request hit)

Minimal API: 510 µs

FastEndpoints: 544 µs

Controllers: 620 µs

✅ Minimal API starts fastest. This makes sense: it has the thinnest pipeline.

📊 2. Single Product (micro CRUD call)

Minimal API: 47 µs (21,281 ops/sec)

Controller: 93 µs (10,746 ops/sec)

FastEndpoints: 477 µs (2,097 ops/sec, slower due to endpoint abstraction overhead in this test)

✅ Minimal API crushes here. It's almost 2× faster than controllers, and 10× faster than FastEndpoints.

📊 3. Small Dataset (100 products)

Controller: 2,047 µs (488 ops/sec)

Minimal API: 2,268 µs (441 ops/sec)

FastEndpoints: 2,633 µs (380 ops/sec)

👉 Controllers slightly edge out here, probably because [ApiController] and controller binding are already optimized for batch serialization.

📊 4. Medium Dataset (1,000 products)

Controller: 29,458 µs (34 ops/sec)

Minimal API: 32,038 µs (31 ops/sec)

FastEndpoints: 31,382 µs (32 ops/sec)

👉 All three are in the same ballpark, but Controllers are marginally better for medium-sized serialization.

📊 5. Concurrent Single Requests

Minimal API: 762 µs (1,312 ops/sec)

FastEndpoints: 806 µs (1,241 ops/sec)

Controllers: 996 µs (1,004 ops/sec)

✅ Under concurrency, Minimal API leads, FastEndpoints is close, Controllers lag.

📊 6. Concurrent Small Datasets

Minimal API: 2,945 µs (340 ops/sec)

FastEndpoints: 2,990 µs (334 ops/sec)

Controllers: 4,055 µs (247 ops/sec)

✅ Here Minimal API & FastEndpoints are better at throughput under load compared to Controllers.

🏆 TL;DR

Best for cold start & tiny CRUD ops → Minimal API

Best for small datasets (batch reads) → Controllers (slight edge)

Best under concurrency (throughput) → Minimal API ≈ FastEndpoints, Controllers fall behind.

FastEndpoints lags on single CRUD microbenchmarks but catches up under concurrency and medium dataset loads.

⚡ So:

Use Minimal APIs for microservices, very fast CRUD, low overhead.

Use Controllers when you want stronger conventions, model validation, better serializer defaults, and large dataset stability.

Use FastEndpoints if you want a structured but still "minimal-ish" framework (good balance under concurrency).

⚙️ Methodology

1. Dataset Preparation

Each framework is seeded with 10,000+ Product objects stored in a thread-safe ConcurrentDictionary<int, Product>.

A shared AtomicCounter ensures that new product IDs are unique when we test POST operations.

This guarantees that all 3 frameworks are tested on the exact same data and under the same storage constraints.

👉 Why important?
If you didn’t normalize the dataset, one framework might appear faster just because it has fewer records to serialize.

2. Types of Benchmarks

The suite is divided into categories (via [BenchmarkCategory]):

🔹 ColdStart

Each framework spins up a fresh WebApplicationFactory + in-memory host.

Then a first request is made (GET /products/1).

This measures:

Framework bootstrapping overhead

Routing + DI setup

First-time JSON serialization cost

🔹 SingleRequest

Makes one GET for a random product between 1–1000.

Measures latency of a single CRUD operation in a “hot” app (already running).

Simulates a low-traffic API.

🔹 SmallDataset (1,000 records)

GET /products/list?count=1000

Forces serialization of 1,000 products.

Tests JSON serialization throughput + memory allocations.

Simulates dashboard/table requests where users fetch lots of rows.

🔹 MediumDataset (10,000 records)

Same as above but with 10,000 records.

Really stresses the serializer + response streaming.

Simulates reporting endpoints or bulk exports.

🔹 Throughput

Two subtypes:

Concurrent Single Requests

Fires 50 concurrent GET /products/{id} calls.

Measures framework request handling concurrency.

Simulates a production system under real user traffic.

Concurrent Dataset Requests

Fires 10 concurrent GET /products/list?count=1000 calls.

Stresses serialization + parallel request handling.

Simulates multiple users downloading big tables at once.

3. BenchmarkDotNet Harness

Uses [MemoryDiagnoser] → tracks allocations per request (important for JSON heavy workloads).

Uses [ThreadingDiagnoser] → shows if threads are blocking each other under concurrent load.

Each method runs hundreds or thousands of iterations → BenchmarkDotNet discards warmup runs and reports stable median numbers.

Everything runs in-process with in-memory test servers → no network noise, only framework overhead.

4. What We’re Actually Comparing

Controller vs Minimal API vs FastEndpoints in terms of:

Startup cost (cold start)

Latency per CRUD request

Serialization throughput (small/medium datasets)

Scalability under concurrency

Memory allocations

So this is not about “real HTTP traffic across the network,” it’s about framework overhead inside the same process.

✅ That means:

If Minimal APIs are faster → it’s because of lower routing/DI overhead.

If Controllers use more memory → it’s from MVC pipeline extras.

# Real Result number 2: Minimal API only

🧪 Methodology (Minimal API Benchmark)

This benchmark isolates Minimal APIs only, with scenarios matching typical CRUD + query workloads:

ColdStartSingleRequest

Spins up a fresh Minimal API host and executes a single GET /products/1.

Measures framework startup + first-request overhead.

CRUD Benchmarks

CreateProduct → POST /products

UpdateProduct → PUT /products/{id}

DeleteProduct → DELETE /products/{id} (here marked NA, probably failed setup or no product existed).

Measure request/response latency and allocation.

Dataset Queries

GetSingleProduct → simple GET /products/{id}.

GetSmallDataset → GET /products/list?count=1000.

GetMediumDataset → GET /products/list?count=10000.

Measure serialization/deserialization + dictionary lookups.

Concurrent/Throughput

ConcurrentSingleRequests → 50 parallel requests for single products.

ConcurrentSmallDatasets → 10 parallel requests, each asking for 1,000 products.

Measures how the Minimal API pipeline scales under concurrent load.

All tests were run on .NET 9.0 with an Intel i7-8650U, using BenchmarkDotNet’s rigorous harness (warmup, iteration, error/stddev tracking, memory allocation tracking).

📊 Results Breakdown
🔹 Cold Start

14.6 ms (14,594 µs) → That’s the cost of spinning up a Minimal API with DI + routing and serving the first request.

Allocations: ~850 KB → typical overhead for host + serializer initialization.

🔹 CRUD

CreateProduct: ~2.7 ms per request, 14.6 KB allocated.

Heavier than a simple GET because it involves ID assignment + writing to the dictionary.

UpdateProduct: ~104 µs (!), very lightweight.

Just updates fields in memory and serializes JSON.

DeleteProduct: NA → likely due to product not existing in seeded data.

👉 Interpretation: Updates are extremely cheap, creates are moderately expensive, deletes need fixing in test setup.

🔹 Single Request

GetSingleProduct: 65 µs → very fast, ~11 KB allocated (JSON serialization cost).

Confirms Minimal API routing overhead is negligible for single GETs.

🔹 Dataset Queries

GetSmallDataset (1k records): 33 ms

GetMediumDataset (10k records): 33 ms (almost identical!)

Allocations: ~10 MB in both.

👉 Interpretation: The bottleneck here is JSON serialization, not request routing. Once you serialize a few thousand objects, the curve flattens because the serializer dominates cost. That’s why 1k vs 10k look the same.

🔹 Concurrent Throughput

ConcurrentSingleRequests: ~1.0 ms total per batch of 50 requests (so ~20 µs per request).

Allocated ~535 KB total.

Minimal APIs handle concurrency very efficiently.

ConcurrentSmallDatasets: ~44 ms per batch of 10 parallel dataset calls.

Allocated ~45 MB (!).

Confirms dataset serialization is the memory/CPU killer.

📝 Key Takeaways

Cold start is relatively heavy (14 ms, 850 KB), but this is expected for any ASP.NET Core host. Once warmed up, performance is excellent.

Single GETs are blazing fast (~65 µs, 11 KB allocations).

Create/Update behave differently:

Update is cheap (~100 µs).

Create has extra cost (~2.7 ms, more allocations).

Dataset queries are serialization-bound:

1k vs 10k records → nearly same latency, dominated by JSON.

Memory allocations jump into tens of MBs.

Concurrency scaling is good for small requests, but bulk queries under concurrency consume massive memory and increase latency sharply.

# Real Result number 3: FastEndpoint API only

🧪 Methodology (FastEndpoints Benchmark)

This benchmark tests the same set of operations as your Minimal API benchmark, but running through the FastEndpoints framework. FastEndpoints provides strongly-typed endpoints with DI, validation, and middleware baked in.

The workload includes:

ColdStartSingleRequest → Start FastEndpoints host and serve one request.

CRUD Operations:

CreateProduct (POST)

UpdateProduct (PUT)

DeleteProduct (DELETE) → failed here (NA, probably due to dataset setup).

Dataset Queries:

GetSingleProduct (GET /{id})

GetSmallDataset (GET /list?count=1000)

GetMediumDataset (GET /list?count=10000)

Concurrency Tests:

ConcurrentSingleRequests → multiple parallel GETs for one product.

ConcurrentSmallDatasets → parallel list queries.

📊 Results Breakdown
🔹 Cold Start

35 ms (35,097 µs) → More than 2× slower than Minimal APIs (14 ms).

Memory: ~2.6 MB vs ~850 KB for Minimal.
👉 Extra overhead comes from FastEndpoints’ startup pipeline (endpoint discovery, validators, etc.).

🔹 CRUD

CreateProduct: ~991 µs (~1 ms), 16.8 KB allocated.

Faster than Minimal API’s 2.7 ms, despite slightly higher allocation.

UpdateProduct: 126 µs, 16 KB allocated.

Comparable to Minimal API’s 104 µs.

DeleteProduct: NA, test issue (same as Minimal API).

👉 Interpretation: FastEndpoints is faster on create, and similar on update, showing good optimization for request binding + model handling.

🔹 Single Request

GetSingleProduct: 73 µs, ~12 KB allocated.

Almost identical to Minimal API (65 µs).

👉 Interpretation: Routing overhead is negligible in both frameworks.

🔹 Dataset Queries

GetSmallDataset (1k records): 3.4 ms, 727 KB allocated.

~10× faster and ~14× less memory than Minimal API (33 ms, 10 MB).

GetMediumDataset (10k records): 31 ms, 10 MB allocated.

Similar to Minimal API (33 ms, 10 MB).

👉 Interpretation: For smaller datasets, FastEndpoints is dramatically more efficient. For larger datasets, both hit the JSON serialization bottleneck.

🔹 Concurrency

ConcurrentSingleRequests: 977 µs, 587 KB allocated.

Nearly identical to Minimal API (1,010 µs, 535 KB).

ConcurrentSmallDatasets: 5.9 ms, 5.3 MB allocated.

~7× faster and ~9× less memory than Minimal API (44 ms, 46 MB).

👉 Interpretation: FastEndpoints is much more memory-efficient and scales better with parallel dataset queries.

📝 Key Takeaways

Cold Start Overhead

FastEndpoints is slower to boot (35 ms vs 14 ms, ~3× allocations).

CRUD Performance

Create is faster than Minimal API (~1 ms vs 2.7 ms).

Update is about the same.

Single GETs

Both are lightning fast (<100 µs).

Dataset Queries

Small dataset (1k) → FastEndpoints is far superior (3 ms vs 33 ms).

Medium dataset (10k) → both dominated by serialization cost (~31–33 ms).

Concurrency Scaling

Single requests: similar.

Small dataset concurrency: FastEndpoints wins big (5.9 ms vs 44 ms, much less memory churn).

✅ In short:

FastEndpoints has higher cold start overhead,

but better request handling and memory efficiency,

especially for dataset queries and concurrency.

# Real Result number 4: Controller API only

🧪 Methodology (ProductsControllerBenchmark)

This benchmark measures the performance of traditional ASP.NET Core Controllers (the MVC-style routing with attributes and controllers).

The tests run the same workload as Minimal APIs and FastEndpoints:

ColdStartSingleRequest – measure startup + first request.

CRUD operations – CreateProduct, UpdateProduct, DeleteProduct.

Dataset queries – GetSingleProduct, GetSmallDataset (1k), GetMediumDataset (10k).

Concurrency tests – ConcurrentSingleRequests, ConcurrentSmallDatasets.

This ensures apples-to-apples comparisons between the 3 frameworks.

📊 Results Breakdown
🔹 Cold Start

17 ms (17,152 µs), ~1.25 MB allocated.

Slightly slower than Minimal APIs (14 ms), but much faster than FastEndpoints (35 ms).
👉 Overhead comes from controller discovery + attribute routing, but less than FastEndpoints’ DI/validation bootstrapping.

🔹 CRUD

CreateProduct: 975 µs (~1 ms), 22 KB allocated.

On par with FastEndpoints (990 µs) and faster than Minimal APIs (2.7 ms).

UpdateProduct: 146 µs, 21 KB allocated.

Similar to FastEndpoints (126 µs) and Minimal (104 µs).

DeleteProduct: NA (dataset setup issue, same across frameworks).

👉 Interpretation: Controller CRUD is efficient and comparable to FastEndpoints.

🔹 Single Request

GetSingleProduct: 83 µs, 15 KB allocated.

Slightly slower than Minimal API (65 µs) and FastEndpoints (73 µs).
👉 Attribute routing adds tiny overhead.

🔹 Dataset Queries

GetSmallDataset (1k records): 3.3 ms, 740 KB allocated.

Virtually identical to FastEndpoints (3.4 ms, 727 KB).

Much faster and lighter than Minimal API (33 ms, 10 MB).

GetMediumDataset (10k records): 22 ms, 10 MB allocated.

Faster than both Minimal API (33 ms) and FastEndpoints (31 ms).

👉 Controllers do very well on dataset queries, especially medium-size sets.

🔹 Concurrency

ConcurrentSingleRequests: 1.1 ms, 724 KB allocated.

Slightly slower than FastEndpoints (977 µs, 587 KB), similar to Minimal APIs (1.0 ms, 535 KB).

ConcurrentSmallDatasets: 5.7 ms, 5.3 MB allocated.

Nearly identical to FastEndpoints (5.9 ms, 5.3 MB).

Much better than Minimal APIs (44 ms, 46 MB).

👉 Controllers scale well under load, closer to FastEndpoints than Minimal APIs.

📝 Key Takeaways

Cold Start

Controllers are middle-ground: slower than Minimal APIs, faster than FastEndpoints.

CRUD

Very competitive (~1 ms for Create, ~146 µs for Update).

Allocations are slightly higher than FastEndpoints.

Single GET

Small overhead vs Minimal APIs (83 µs vs 65 µs).

Dataset Queries

Small datasets: tied with FastEndpoints (~3 ms).

Medium datasets: Controllers are fastest (22 ms vs 31–33 ms).

Concurrency

Single request concurrency: slightly behind FastEndpoints.

Dataset concurrency: tied with FastEndpoints, both far ahead of Minimal APIs.

✅ In summary:

Controllers are a balanced middle option.

Cold start + routing overhead is modest.

Dataset handling and concurrency scaling are as good as or better than FastEndpoints.

CRUD speed is competitive across the board.

# Real results 5: API Framework Comparison Benchmark

🎯 Purpose

This benchmark directly compares three API implementation styles in .NET 9:

Controller-based APIs (traditional ASP.NET Core MVC controllers).

Minimal APIs (introduced in .NET 6, lightweight request handlers).

FastEndpoints (a popular community library optimized for minimal overhead and high throughput).

The goal is to see how each framework performs across different workloads.

🧪 Methodology

1. Cold Start Scenarios

Controller_ColdStart, MinimalApi_ColdStart, FastEndpoints_ColdStart

Each framework is measured for its first request latency after application startup (JIT, routing initialization, middleware warmup).

Helps evaluate framework overhead.

2. Dataset Retrieval

GetSingleProduct → fetch 1 item.

GetSmallDataset → return a small list (~hundreds of items).

GetMediumDataset → return a medium dataset (~thousands of items).

This tests query handling, serialization, and allocation overhead.

3. CRUD Operations

(Not all CRUD shown here, but Create/Update/Delete are typically included in other parts of the suite.)

4. Concurrent Requests (Throughput)

ConcurrentSingleRequests → multiple clients hitting single-product endpoint simultaneously.

ConcurrentSmallDatasets → multiple clients requesting small dataset concurrently.

Tests thread scheduling, resource contention, and scalability.

📊 What the Results Show

Cold Start:
Minimal API has the lowest latency (~17 ms) vs Controller (~24 ms). FastEndpoints is slightly higher (~19 ms) but allocates much more memory.

Single Product:
Minimal API is fastest (32 µs), FastEndpoints second (56 µs), Controller slowest (78 µs).

Small Dataset:
Controller and FastEndpoints perform similarly (~2 ms), but Minimal API is much slower (23 ms) and allocates way more memory (10 MB vs ~0.7 MB).

Medium Dataset:
Controller (~25 ms) beats FastEndpoints (~33 ms) and Minimal API (~42 ms).
All three allocate a lot (~10 MB), but Minimal API tends to allocate slightly more.

Throughput / Concurrency:

For single concurrent requests, Minimal API is the fastest (~650 µs).

For small dataset concurrency, Controller and FastEndpoints (~4 ms) vastly outperform Minimal API (~38 ms, with huge allocations of ~90 MB).

🧾 Interpretation

Minimal API is great for lightweight endpoints (single product) and cold start, but struggles badly under heavier payloads and concurrency, due to serialization + allocation overhead.

Controller APIs are stable, consistent, and memory-efficient for datasets.

FastEndpoints is competitive, sometimes faster than Controllers, but has higher cold start costs and allocations in some cases.

# Real results TLDR

you’ve got 5 reports:

NBomber load test (system-level, concurrency & throughput).

ProductsControllerBenchmark (controllers only).

ProductsFastEndpointsBenchmark (FastEndpoints only).

ProductsMinimalApiBenchmark (Minimal API only).

ApiPerformanceComparisonBenchmark (all 3 compared directly).

Instead of treating them separately, we can create one consolidated benchmark report that:

Introduces methodology & datasets once (no repetition).

Breaks results into sections per workload (ColdStart, CRUD, Single, Dataset, Concurrency).

Shows per-framework detail (Controller / Minimal API / FastEndpoints) → pulling numbers from the framework-specific reports.

Summarizes with direct comparisons (from the comparison benchmark + NBomber load tests).

Ends with recommendations & use-case mapping.

📊 Proposed Consolidated Report Structure

1. Introduction

Purpose of the benchmarks (compare Controllers, Minimal API, FastEndpoints).

Tools: BenchmarkDotNet (microbenchmarking), NBomber (load testing).

Hardware & environment specs (same for all tests).

Dataset definitions (Single, Small, Medium).

2. Cold Start Performance

Numbers from each dedicated benchmark (controller, minimal, fastendpoints).

Show comparison from the combined benchmark.

Insight: Minimal API fastest, Controllers slower but stable, FastEndpoints higher allocations.

3. CRUD Operations (Create / Update / Delete)

Pull from framework-specific reports (since the comparison one didn’t test CRUD).

Note Delete gaps (NA results).

Show allocations + mean latencies.

4. Single Product Requests

Show microbenchmark results (mean µs + allocations).

Tie into NBomber throughput test (requests/sec).

5. Dataset Retrieval (Small vs Medium)

Combine framework-specific reports + cross-framework benchmark.

Discuss scaling behavior (Minimal API worst on small dataset, Controllers/FE efficient).

6. Concurrency & Throughput

BenchmarkDotNet concurrency results (ConcurrentSingle, ConcurrentSmallDataset).

NBomber system-level load test (requests/sec, latency percentiles).

Cross-validate: BDN latency vs NBomber throughput.

7. Memory & Allocations

Compare GC pressure across frameworks.

Highlight Minimal API memory blowup under concurrency.

Show relative efficiency of Controllers & FastEndpoints.

8. Summary & Recommendations

Minimal API → best for small, stateless, microservices.

Controllers → balanced, predictable, good for dataset-heavy apps.

FastEndpoints → competitive under load, slightly higher allocations.

Practical guidance: which to choose depending on product type.

# How to run NBombmer, load tests?

How to run your load testing project

Make sure you have your APIs running:

Controllers → http://localhost:5001

Minimal API → http://localhost:5002

FastEndpoints → http://localhost:5003

Go to your load test project folder (where you put ApiFrameworkLoadTests).

cd path/to/ApiPerformanceComparison.LoadTests

Run it with dotnet run:

dotnet run

You’ll see the prompt:

# SAFE API Framework Load Testing (Laptop-Friendly)

Select test to run:

1. Basic Capacity Test
2. Spike Test
3. Mixed Workload Test
4. Breaking Point Test
   Enter choice (1-4):

Enter 1, 2, 3, or 4 depending on the test you want.

After it finishes, check the results:

Reports are generated in folders like:

load_test_results_safe/

spike_test_results_safe/

mixed_workload_results_safe/

breaking*point_results_safe*\*

You’ll find both HTML reports and CSV files.

# FINAL RESULTS

## Combined Frameworks

🔎 High-Level Observations

Cold Start

Minimal API (~11.3 ms) and Controller (~10.8 ms) are very close.

FastEndpoints (~17.6 ms) is significantly slower at startup, and allocates about 2–3x more memory.

✅ This matches expectations: FastEndpoints does extra bootstrapping for DI and endpoint discovery.

Single Request (GetSingleProduct)

Minimal API is fastest at ~29 µs.

FastEndpoints is ~60 µs, roughly 2x slower.

Controller is the slowest at ~76 µs.

✅ Allocations follow the same pattern: Minimal API allocates the least (11 KB), Controller the most (15 KB).

Small Dataset (single request, ~2.2–2.3 ms)

All three frameworks perform almost identically: Minimal API ~2,266 µs, Controller ~2,315 µs, FastEndpoints ~2,293 µs.

✅ Allocations are nearly the same (~810 KB).

👉 This shows the dataset size dominates runtime, not framework overhead.

Medium Dataset (single request, ~22 ms)

Again, all three are nearly identical: ~22,300–22,500 µs.

Allocations are ~10 MB across the board.

✅ Framework overhead becomes negligible at this scale.

Concurrent Single Requests

Minimal API leads at ~619 µs.

FastEndpoints follows at ~696 µs.

Controller lags at ~774 µs.

✅ Memory allocations align: Minimal API is most efficient (~531 KB), Controller the least (~719 KB).

Concurrent Small Dataset Requests

Minimal API and Controller are tied at ~6.28 ms.

FastEndpoints lags slightly (~6.8 ms).

✅ Allocations are high across the board (~5.8–6 MB), with negligible differences.

📊 Performance Profile

Minimal API

🟢 Best for single-request latency (both single product and concurrent requests).

🟡 Scales well under load, with lowest allocations overall.

🔴 Slightly higher cold start time than Controllers, but not by much.

Controllers

🟢 Competitive for larger dataset requests.

🟡 Cold start is slightly better than Minimal API.

🔴 Worst at single-request latency and memory efficiency.

FastEndpoints

🟢 Stronger than Controllers in concurrent single-request throughput.

🟡 Matches the others on dataset-heavy requests.

🔴 Slowest cold start and higher memory allocations.

💡 Key Insights

For microservices or APIs with frequent small calls → Minimal API is the clear winner (lowest latency and memory use).

For enterprise apps with Controllers already in place → Controllers are fine; performance differences shrink once requests return real datasets.

For teams using FastEndpoints for organization and maintainability → The overhead is acceptable, since real-world dataset-heavy endpoints erase most of the gap.
