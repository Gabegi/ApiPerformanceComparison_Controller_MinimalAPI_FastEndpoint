using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace ApiPerformanceComparison.LoadTesting.LoadTesting
{
    public class ApiFrameworkLoadTests : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _reportBaseDir;

        public ApiFrameworkLoadTests()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };

            // Resolve absolute path inside project
            var projectDir = AppContext.BaseDirectory;
            var root = Path.Combine(projectDir, "..", "..", ".."); // back to project root
            _reportBaseDir = Path.GetFullPath(Path.Combine(root, "loadtestingreports"));

            Directory.CreateDirectory(_reportBaseDir); // ensure it exists
        }

        // =========================
        // BASIC CAPACITY TEST
        // =========================
        public void RunBasicCapacityTest(string endpoint)
        {
            var scenario = Scenario.Create($"basic_capacity_{GetName(endpoint)}", async context =>
            {
                var productId = Random.Shared.Next(1, 1000);
                try
                {
                    var response = await _httpClient.GetAsync($"{endpoint}/products/{productId}");
                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 5, during: TimeSpan.FromSeconds(10)),
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(15)),
                Simulation.KeepConstant(copies: 20, during: TimeSpan.FromSeconds(15))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder(Path.Combine(_reportBaseDir, GetName(endpoint), "basic_capacity"))
                .WithReportFormats(ReportFormat.Csv)
                .Run();
        }

        // =========================
        // SPIKE TEST
        // =========================
        public void RunSpikeTest(string endpoint)
        {
            var scenario = Scenario.Create($"spike_test_{GetName(endpoint)}", async context =>
            {
                var productId = Random.Shared.Next(1, 1000);
                try
                {
                    var response = await _httpClient.GetAsync($"{endpoint}/products/{productId}");
                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 5, during: TimeSpan.FromSeconds(10)),   // baseline
                Simulation.KeepConstant(copies: 20, during: TimeSpan.FromSeconds(10)),  // spike
                Simulation.KeepConstant(copies: 5, during: TimeSpan.FromSeconds(10))    // recovery
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder(Path.Combine(_reportBaseDir, GetName(endpoint), "spike_test"))
                .WithReportFormats(ReportFormat.Csv)
                .Run();
        }

        // =========================
        // MIXED WORKLOAD TEST
        // =========================
        public void RunMixedWorkloadTest(string endpoint)
        {
            var scenario = Scenario.Create($"mixed_workload_{GetName(endpoint)}", async context =>
            {
                var workloadType = Random.Shared.NextDouble();
                try
                {
                    if (workloadType < 0.7) // 70% single product
                    {
                        var productId = Random.Shared.Next(1, 1000);
                        var response = await _httpClient.GetAsync($"{endpoint}/products/{productId}");
                        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                    }
                    else if (workloadType < 0.9) // 20% small dataset
                    {
                        var response = await _httpClient.GetAsync($"{endpoint}/products/list?count=100");
                        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                    }
                    else // 10% medium dataset
                    {
                        var response = await _httpClient.GetAsync($"{endpoint}/products/list?count=1000");
                        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                    }
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(20)),
                Simulation.KeepConstant(copies: 20, during: TimeSpan.FromSeconds(20)),
                Simulation.KeepConstant(copies: 30, during: TimeSpan.FromSeconds(20))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder(Path.Combine(_reportBaseDir, GetName(endpoint), "mixed_workload"))
                .WithReportFormats(ReportFormat.Csv)
                .Run();
        }

        // =========================
        // BREAKING POINT TEST
        // =========================
        public void RunBreakingPointTest(string endpoint)
        {
            var scenario = Scenario.Create($"breaking_point_{GetName(endpoint)}", async context =>
            {
                var productId = Random.Shared.Next(1, 1000);
                try
                {
                    var response = await _httpClient.GetAsync($"{endpoint}/products/{productId}");
                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.Inject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(10)),
                Simulation.Inject(rate: 15, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(10)),
                Simulation.Inject(rate: 30, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(10)),
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(10))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder(Path.Combine(_reportBaseDir, GetName(endpoint), "breaking_point"))
                .WithReportFormats(ReportFormat.Csv)
                .Run();
        }

        private string GetName(string url)
        {
            try
            {
                var uri = new Uri(url);
                return $"{uri.Host}_{uri.Port}";
            }
            catch
            {
                return url.Replace(":", "_").Replace("/", "_");
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
