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

# Data 

## Why SQLite + EF Core is Perfect for This ProjectAdvantages:

Consistent Database Layer: All three API types use the same data access pattern
Realistic Performance Testing: Database I/O is often the bottleneck in real applications
Easy Setup: No external database server required
Reproducible: Same database file across all tests
Isolated: Each test can use fresh data
Fast: SQLite is very fast for read-heavy workloads typical in benchmarks

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