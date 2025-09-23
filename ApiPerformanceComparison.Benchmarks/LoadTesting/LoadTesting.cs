using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;

namespace ApiPerformanceComparison.Benchmarks.LoadTesting
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
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        // =========================
        // BASIC CAPACITY TEST
        // =========================
        public void RunBasicCapacityTest()
        {
            // Test each endpoint separately
            var scenario1 = Scenario.Create("basic_controllers", async context =>
            {
                var productId = Random.Shared.Next(1, 1000);
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoints[0]}/products/{productId}");
                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),
                Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),
                Simulation.Inject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2))
            );

            var scenario2 = Scenario.Create("basic_minimal_api", async context =>
            {
                var productId = Random.Shared.Next(1, 1000);
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoints[1]}/products/{productId}");
                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),
                Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),
                Simulation.Inject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2))
            );

            var scenario3 = Scenario.Create("basic_fast_endpoints", async context =>
            {
                var productId = Random.Shared.Next(1, 1000);
                try
                {
                    var response = await _httpClient.GetAsync($"{_endpoints[2]}/products/{productId}");
                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
                }
                catch
                {
                    return Response.Fail();
                }
            })
            .WithLoadSimulations(
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),
                Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),
                Simulation.Inject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2))
            );

            NBomberRunner
                .RegisterScenarios(scenario1, scenario2, scenario3)
                .WithReportFolder("load_test_results")
                .WithReportFormats(ReportFormat.Html, ReportFormat.Csv)
                .Run();
        }

        // =========================
        // SPIKE TEST
        // =========================
        public void RunSpikeTest()
        {
            var spikeScenario = Scenario.Create("spike_test", async context =>
            {
                var endpoint = _endpoints[Random.Shared.Next(_endpoints.Length)];
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
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2)),   // baseline
                Simulation.Inject(rate: 500, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),  // spike
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(2))    // recovery
            );

            NBomberRunner
                .RegisterScenarios(spikeScenario)
                .WithReportFolder("spike_test_results")
                .WithReportFormats(ReportFormat.Html)
                .Run();
        }

        // =========================
        // MIXED WORKLOAD TEST
        // =========================
        public void RunMixedWorkloadTest()
        {
            var mixedScenario = Scenario.Create("mixed_workload", async context =>
            {
                var endpoint = _endpoints[Random.Shared.Next(_endpoints.Length)];
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
                Simulation.KeepConstant(copies: 25, during: TimeSpan.FromMinutes(1)),
                Simulation.KeepConstant(copies: 50, during: TimeSpan.FromMinutes(3)),
                Simulation.KeepConstant(copies: 100, during: TimeSpan.FromMinutes(3)),
                Simulation.KeepConstant(copies: 150, during: TimeSpan.FromMinutes(2))
            );

            NBomberRunner
                .RegisterScenarios(mixedScenario)
                .WithReportFolder("mixed_workload_results")
                .WithReportFormats(ReportFormat.Html, ReportFormat.Csv)
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
                Simulation.Inject(rate: 50, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.Inject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.Inject(rate: 500, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.Inject(rate: 1000, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1)),
                Simulation.Inject(rate: 2000, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1))
            );

            NBomberRunner
                .RegisterScenarios(scenario)
                .WithReportFolder($"breaking_point_results_{GetName(endpoint)}")
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

            Console.WriteLine("API Framework Load Testing");
            Console.WriteLine("==========================");
            Console.WriteLine("Select test to run:");
            Console.WriteLine("1. Basic Capacity Test");
            Console.WriteLine("2. Spike Test");
            Console.WriteLine("3. Mixed Workload Test");
            Console.WriteLine("4. Breaking Point Test");
            Console.Write("Enter choice (1-4): ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Running Basic Capacity Test...");
                        loadTester.RunBasicCapacityTest();
                        break;
                    case "2":
                        Console.WriteLine("Running Spike Test...");
                        loadTester.RunSpikeTest();
                        break;
                    case "3":
                        Console.WriteLine("Running Mixed Workload Test...");
                        loadTester.RunMixedWorkloadTest();
                        break;
                    case "4":
                        Console.WriteLine("Running Breaking Point Test for each framework separately...");
                        var endpoints = new[]
                        {
                            "http://localhost:5001",
                            "http://localhost:5002",
                            "http://localhost:5003"
                        };

                        foreach (var endpoint in endpoints)
                        {
                            Console.WriteLine($"Testing {endpoint}...");
                            loadTester.RunBreakingPointTest(endpoint);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Running Basic Capacity Test...");
                        loadTester.RunBasicCapacityTest();
                        break;
                }

                Console.WriteLine("Load test completed! Check the report folders for results.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running load test: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}