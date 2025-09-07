using ApiPerformanceComparison.Shared;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// seed and DI
var products = QuickSeeder.SeedProducts(100_000);
builder.Services.AddSingleton(products);

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.EnableJWTBearer = false;
    o.DocumentSettings = s => s.Title = "API Performance Comparison - FastEndpoints";
});

var app = builder.Build();

if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseHttpsRedirection();
}

app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();
