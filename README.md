# ApiPerformanceComparison_Controller_MinimalAPI_FastEndpoint

# Goals

Compare Controllers vs Minimal APIs vs FastEndpoints in terms of performance only

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

tool: NBomber

# Project Structure
