using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace ApiPerformanceComparison.LoadTesting.LoadTesting
{
    public class ApiFrameworkLoadTests : IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly string[] _endpoints = new[]
        {
            "http://localhost:5001", // Controllers
            "http://localhost:5002", // Minimal API
            "http://localhost:5003"  // FastEndpoints
        };

        public ApiFrameworkLoadTests()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        // =========================
        // BASIC CAPACITY TEST (safe)
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
                Simulation.KeepConstant(copies: 5, during: TimeSpan.FromSeconds(15)),
                Simulation.KeepConstant(copies: 10, during: TimeSpan.FromSeconds(15)),
                Simulation.KeepConstant(copies: 25, during: TimeSpan.FromSeconds(20)),
                Simulation.KeepConstant(copies: 50, during: TimeSpan.FromSeconds(20))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder("load_test_results_safe")
                .WithReportFormats(ReportFormat.Html, ReportFormat.Csv)
                .Run();
        }

        // =========================
        // SPIKE TEST (safe)
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
                Simulation.KeepConstant(copies: 50, during: TimeSpan.FromSeconds(10)),  // spike
                Simulation.KeepConstant(copies: 5, during: TimeSpan.FromSeconds(10))    // recovery
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder("spike_test_results_safe")
                .WithReportFormats(ReportFormat.Html)
                .Run();
        }

        // =========================
        // MIXED WORKLOAD TEST (safe)
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
                Simulation.KeepConstant(copies: 25, during: TimeSpan.FromSeconds(20)),
                Simulation.KeepConstant(copies: 50, during: TimeSpan.FromSeconds(20))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder("mixed_workload_results_safe")
                .WithReportFormats(ReportFormat.Html, ReportFormat.Csv)
                .Run();
        }

        // =========================
        // BREAKING POINT TEST (safe)
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
                Simulation.Inject(rate: 5, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15)),
                Simulation.Inject(rate: 20, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15)),
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15)),
                Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(15))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder($"breaking_point_results_safe_{GetName(endpoint)}")
                .WithReportFormats(ReportFormat.Html)
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

    // =========================
    // CONSOLE ENTRY POINT
    // =========================
    public class Program
    {
        public static void Main(string[] args)
        {
            using var loadTester = new ApiFrameworkLoadTests();

            Console.WriteLine("SAFE API Framework Load Testing (Laptop-Friendly)");
            Console.WriteLine("================================================");
            Console.WriteLine("Select test to run:");
            Console.WriteLine("1. Basic Capacity Test");
            Console.WriteLine("2. Spike Test");
            Console.WriteLine("3. Mixed Workload Test");
            Console.WriteLine("4. Breaking Point Test");
            Console.Write("Enter choice (1-4): ");

            var choice = Console.ReadLine();

            var endpoints = new[]
            {
                "http://localhost:5001",
                "http://localhost:5002",
                "http://localhost:5003"
            };

            try
            {
                switch (choice)
                {
                    case "1":
                        foreach (var endpoint in endpoints)
                        {
                            Console.WriteLine($"Running Basic Capacity Test for {endpoint}...");
                            loadTester.RunBasicCapacityTest(endpoint);
                        }
                        break;
                    case "2":
                        foreach (var endpoint in endpoints)
                        {
                            Console.WriteLine($"Running Spike Test for {endpoint}...");
                            loadTester.RunSpikeTest(endpoint);
                        }
                        break;
                    case "3":
                        foreach (var endpoint in endpoints)
                        {
                            Console.WriteLine($"Running Mixed Workload Test for {endpoint}...");
                            loadTester.RunMixedWorkloadTest(endpoint);
                        }
                        break;
                    case "4":
                        foreach (var endpoint in endpoints)
                        {
                            Console.WriteLine($"Running Breaking Point Test for {endpoint}...");
                            loadTester.RunBreakingPointTest(endpoint);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Running Basic Capacity Test on Controllers...");
                        loadTester.RunBasicCapacityTest(endpoints[0]);
                        break;
                }

                Console.WriteLine("✅ Load test completed! Check the report folders (ending in _safe) for results.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error running load test: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}