# ApiPerformanceComparison – Controllers vs Minimal APIs vs FastEndpoints

This project benchmarks three different API implementation styles in .NET 9:

- Controllers (ASP.NET Core MVC style)

- Minimal APIs (introduced in .NET 6)

- FastEndpoints
(community library optimised for high throughput)

The goal is to compare them in terms of performance only.
Same host, same runtime, same payloads, same data store.
Only the framework surface differs.

# 🎯 Goals

Measure throughput, latency, allocations, and scalability.

Use controlled, reproducible datasets.

Benchmark both micro (CRUD) and macro (dataset/concurrency) scenarios.

Provide actionable insights for framework choice.

# 🚀 What Does Performance Mean?

In API contexts, performance is not just raw response time:

- Throughput – requests per second.

- Latency – average, P95, P99 response times.

- Memory allocations – bytes allocated per request.

- CPU usage – efficiency under concurrency.

- Startup time – readiness after app launch.

- Cold start latency – cost of first request.

# 🧪 Benchmark Methodology

Tools

BenchmarkDotNet
: micro-benchmarks

NBomber / K6 / Bombardier: load testing

dotnet-counters / dotnet-trace / PerfView: runtime diagnostics

Dataset

Instead of EF Core or Bogus, we use fixed in-memory products:

Small (1,000 items): simulates paginated responses.

Medium (10,000 items): stresses serialization + memory.

Avoid Large (>50k): skews results (serialization dominates).

👉 Deterministic, lightweight, reproducible.

API Operations

Benchmarks include:

Simple GET (/products/1)

GET with params (/products/{id}, /products?category=...)

POST with JSON (insert product)

PUT/PATCH with validation (update product)

Dataset queries (1k & 10k items)

Load Testing Scenarios

Constant Load – steady traffic

Spike Load – sudden bursts

Ramp-Up Load – gradual increase

Mixed Workload – CRUD + datasets + concurrency

⚙️ Test Environment

CPU: Intel i7-8650U (4c/8t, 1.9–2.1 GHz, laptop class)

Runtime: .NET 9.0.9 (RyuJIT AVX2)

OS: Windows 11 (24H2)

👉 Latency and GC overhead are more visible on laptop hardware.

📊 Results

1. Cold Start

Minimal API: ~11–17 ms (fastest)

Controller: ~17–24 ms (middle)

FastEndpoints: ~30–35 ms (slowest, ~3× allocations)

2. Single CRUD Requests

Minimal API: 29–65 µs (fastest)

FastEndpoints: 60–70 µs

Controller: 75–85 µs (slowest)

3. Dataset Queries

Small (1k):

Controller ≈ FastEndpoints (~3 ms, ~740 KB)

Minimal API (~23–33 ms, ~10 MB) → struggles

Medium (10k):

Controller: ~22 ms (fastest)

FastEndpoints: ~31 ms

Minimal API: ~33–42 ms

4. Concurrency / Throughput

Concurrent Single Requests:

Minimal API best (~619 µs)

FastEndpoints close (~696 µs)

Controller slower (~774 µs)

Concurrent Small Datasets:

Controller ≈ FastEndpoints (~6 ms, ~5 MB)

Minimal API (~38–44 ms, ~45 MB) → falls behind

📝 Key Takeaways

Minimal API
🟢 Best for microservices, CRUD, and high-concurrency small requests.
🔴 Suffers on dataset-heavy endpoints (serialization & allocations).

Controllers
🟢 Stable, predictable, optimized for datasets.
🟡 Slightly slower per-request, higher allocations.
✅ Great for enterprise apps with existing MVC conventions.

FastEndpoints
🟢 Balanced: structured like Controllers, speed closer to Minimal APIs.
🟡 Cold start is slower, allocations higher.
✅ Excels in concurrency and dataset workloads.

```

📦 Project Structure
ApiPerformanceComparison/
├── src/
│   ├── ApiPerformanceComparison.Controllers/
│   ├── ApiPerformanceComparison.MinimalApi/
│   ├── ApiPerformanceComparison.FastEndpoints/
│   └── ApiPerformanceComparison.Shared/
├── tests/
│   ├── Benchmarks/
│   └── LoadTests/
└── results/
    ├── benchmarks/
    ├── load-tests/
    └── reports/
```

🔬 Unified vs Individual Benchmarks

Individual benchmarks – test each framework in isolation.

Unified benchmark class – runs all 3 side-by-side, ensuring:

Same data

Same runtime state

Direct performance ratios

👉 Keep both:

Individual → regression tracking.

Unified → framework comparison.

🛠️ Running Benchmarks
BenchmarkDotNet
cd tests/ApiPerformanceComparison.Benchmarks
dotnet run -c Release

Load Tests (NBomber)

Make sure APIs are running on ports:

Controllers → http://localhost:5001

Minimal → http://localhost:5002

FastEndpoints → http://localhost:5003

Run load tests:

cd tests/ApiPerformanceComparison.LoadTests
dotnet run

Reports generated under:

results/load-tests/

✅ Recommendations

Choose Minimal APIs for microservices, serverless, and edge workloads where startup time and CRUD latency matter most.

Choose Controllers for enterprise APIs, large datasets, and apps already invested in MVC tooling.

Choose FastEndpoints if you want structured, typed endpoints with high concurrency performance, trading some cold start overhead.
