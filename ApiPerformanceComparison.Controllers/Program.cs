using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Data seeding once at startup
var products = QuickSeeder.SeedProducts(50_000);
builder.Services.AddSingleton(products);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
