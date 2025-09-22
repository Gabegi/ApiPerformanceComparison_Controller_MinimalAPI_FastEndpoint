using ApiPerformanceComparison.Shared;
using System.Collections.Concurrent;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Register services (datasets will be injected by benchmarks)
builder.Services.AddSingleton<Dictionary<int, Product>>();          // for reads
builder.Services.AddSingleton<ConcurrentDictionary<int, Product>>(); // for writes
builder.Services.AddSingleton<AtomicCounter>();

// Register FastEndpoints
builder.Services.AddFastEndpoints();

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.UseFastEndpoints();

app.Run();

namespace ApiPerformanceComparison.FastEndpoints
{
    public sealed class FastEndpointsEntryPoint { }
}
