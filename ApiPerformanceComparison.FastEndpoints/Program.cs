using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Register services (datasets will be injected by benchmarks)
builder.Services.AddSingleton<ConcurrentDictionary<int, Product>>();
builder.Services.AddSingleton<AtomicCounter>();

// Register FastEndpoints
builder.Services.AddFastEndpoints();

var app = builder.Build();

// Enable HTTPS redirection if not in testing
if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

// Use FastEndpoints middleware
app.UseFastEndpoints();

// Run the app
app.Run();

// Entry point marker for FastEndpoints (used by benchmarks)
namespace ApiPerformanceComparison.FastEndpoints
{
    public sealed class FastEndpointsEntryPoint { }
}
