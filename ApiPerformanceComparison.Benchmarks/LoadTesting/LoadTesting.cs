using System;

namespace ApiPerformanceComparison.Benchmarks.LoadTesting;

public class LoadTesting
{
    var scenario = Scenario.Create("api_comparison", async context =>
{
    var endpoint = context.ScenarioInfo.ScenarioName switch
    {
        "controller" => "http://localhost:5001",
        "minimal" => "http://localhost:5002", 
        "fastendpoints" => "http://localhost:5003",
        _ => throw new ArgumentException()
    };
    
    var response = await httpClient.GetAsync($"{endpoint}/products/{Random.Shared.Next(1, 1000)}");
    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
})
.WithLoadSimulations(
    // Find maximum sustainable RPS
    Simulation.KeepConstant(copies: 50, during: TimeSpan.FromMinutes(2)),
    Simulation.KeepConstant(copies: 100, during: TimeSpan.FromMinutes(2)),
    Simulation.KeepConstant(copies: 200, during: TimeSpan.FromMinutes(2))
);
}
