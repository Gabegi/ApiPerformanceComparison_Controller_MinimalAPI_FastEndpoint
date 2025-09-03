using ApiPerformanceComparison.Shared;

var builder = WebApplication.CreateBuilder(args);

// Data seeding once at startup
var products = QuickSeeder.SeedProducts(1000_000);
builder.Services.AddSingleton(products);


// API
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
