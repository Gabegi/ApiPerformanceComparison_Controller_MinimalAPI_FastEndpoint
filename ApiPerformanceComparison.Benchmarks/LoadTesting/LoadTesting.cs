//using NBomber.CSharp;

//namespace ApiPerformanceComparison.Benchmarks.LoadTesting
//{
//    public class ApiFrameworkLoadTests
//    {
//        private readonly HttpClient _httpClient;

//        public ApiFrameworkLoadTests()
//        {
//            _httpClient = new HttpClient
//            {
//                Timeout = TimeSpan.FromSeconds(30)
//            };
//        }

//        public void RunBasicCapacityTest()
//        {
//            var controllerScenario = CreateScenario("controller", "http://localhost:5001");
//            var minimalApiScenario = CreateScenario("minimal_api", "http://localhost:5002");
//            var fastEndpointsScenario = CreateScenario("fastendpoints", "http://localhost:5003");

//            NBomberRunner
//                .RegisterScenarios(controllerScenario, minimalApiScenario, fastEndpointsScenario)
//                .WithReportFolder("load_test_results")
//                .WithReportFormats(ReportFormat.Html, ReportFormat.Csv)
//                .Run();
//        }

//        public void RunSpikeTest()
//        {
//            var spikeScenario = Scenario.Create("spike_test", async context =>
//            {
//                // Test all frameworks under spike conditions
//                var endpoints = new[]
//                {
//                    "http://localhost:5001", // Controller
//                    "http://localhost:5002", // Minimal API
//                    "http://localhost:5003"  // FastEndpoints
//                };

//                var endpoint = endpoints[Random.Shared.Next(endpoints.Length)];
//                var productId = Random.Shared.Next(1, 1000);
                
//                var response = await _httpClient.GetAsync($"{endpoint}/products/{productId}");
//                return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
//            })
//            .WithLoadSimulations(
//                Simulation.InjectPerSec(rate: 50, during: TimeSpan.FromMinutes(2)),   // Baseline
//                Simulation.InjectPerSec(rate: 500, during: TimeSpan.FromMinutes(1)),  // Spike
//                Simulation.InjectPerSec(rate: 50, during: TimeSpan.FromMinutes(2))    // Recovery
//            );

//            NBomberRunner
//                .RegisterScenarios(spikeScenario)
//                .WithReportFolder("spike_test_results")
//                .WithReportFormats(ReportFormat.Html)
//                .Run();
//        }

//        public void RunMixedWorkloadTest()
//        {
//            var mixedWorkloadScenario = Scenario.Create("mixed_workload", async context =>
//            {
//                var endpoint = SelectEndpoint();
//                var workloadType = Random.Shared.NextDouble();

//                try
//                {
//                    if (workloadType < 0.7) // 70% - Single product requests
//                    {
//                        var productId = Random.Shared.Next(1, 1000);
//                        var response = await _httpClient.GetAsync($"{endpoint}/products/{productId}");
//                        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
//                    }
//                    else if (workloadType < 0.9) // 20% - Small dataset requests
//                    {
//                        var response = await _httpClient.GetAsync($"{endpoint}/products/list?count=100");
//                        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
//                    }
//                    else // 10% - Medium dataset requests
//                    {
//                        var response = await _httpClient.GetAsync($"{endpoint}/products/list?count=1000");
//                        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
//                    }
//                }
//                catch (Exception)
//                {
//                    return Response.Fail();
//                }
//            })
//            .WithLoadSimulations(
//                Simulation.KeepConstant(copies: 25, during: TimeSpan.FromMinutes(1)),   // Warm up
//                Simulation.KeepConstant(copies: 50, during: TimeSpan.FromMinutes(3)),   // Baseline load
//                Simulation.KeepConstant(copies: 100, during: TimeSpan.FromMinutes(3)),  // Increased load
//                Simulation.KeepConstant(copies: 150, during: TimeSpan.FromMinutes(2))   // High load
//            );

//            NBomberRunner
//                .RegisterScenarios(mixedWorkloadScenario)
//                .WithReportFolder("mixed_workload_results")
//                .WithReportFormats(ReportFormat.Html, ReportFormat.Csv)
//                .Run();
//        }

//        public void RunBreakingPointTest()
//        {
//            var breakingPointScenario = CreateScenario("breaking_point", "http://localhost:5001"); // Test one at a time

//            NBomberRunner
//                .RegisterScenarios(breakingPointScenario)
//                .WithLoadSimulations(
//                    Simulation.InjectPerSec(rate: 50, during: TimeSpan.FromMinutes(1)),
//                    Simulation.InjectPerSec(rate: 100, during: TimeSpan.FromMinutes(1)),
//                    Simulation.InjectPerSec(rate: 200, during: TimeSpan.FromMinutes(1)),
//                    Simulation.InjectPerSec(rate: 500, during: TimeSpan.FromMinutes(1)),
//                    Simulation.InjectPerSec(rate: 1000, during: TimeSpan.FromMinutes(1)),
//                    Simulation.InjectPerSec(rate: 2000, during: TimeSpan.FromMinutes(1))
//                )
//                .WithReportFolder("breaking_point_results")
//                .WithReportFormats(ReportFormat.Html)
//                .Run();
//        }

//        private Scenario CreateScenario(string name, string baseUrl)
//        {
//            return Scenario.Create(name, async context =>
//            {
//                try
//                {
//                    var productId = Random.Shared.Next(1, 1000);
//                    var response = await _httpClient.GetAsync($"{baseUrl}/products/{productId}");
                    
//                    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
//                }
//                catch (Exception)
//                {
//                    return Response.Fail();
//                }
//            })
//            .WithLoadSimulations(
//                Simulation.KeepConstant(copies: 50, during: TimeSpan.FromMinutes(2)),
//                Simulation.KeepConstant(copies: 100, during: TimeSpan.FromMinutes(2)),
//                Simulation.KeepConstant(copies: 200, during: TimeSpan.FromMinutes(2))
//            );
//        }

//        private string SelectEndpoint()
//        {
//            var endpoints = new[]
//            {
//                "http://localhost:5001", // Controller
//                "http://localhost:5002", // Minimal API
//                "http://localhost:5003"  // FastEndpoints
//            };

//            return endpoints[Random.Shared.Next(endpoints.Length)];
//        }

//        public void Dispose()
//        {
//            _httpClient?.Dispose();
//        }
//    }

//    // Console app entry point for running load tests
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var loadTester = new ApiFrameworkLoadTests();

//            try
//            {
//                Console.WriteLine("API Framework Load Testing");
//                Console.WriteLine("==========================");
//                Console.WriteLine();
//                Console.WriteLine("Select test to run:");
//                Console.WriteLine("1. Basic Capacity Test (Compare all frameworks)");
//                Console.WriteLine("2. Spike Test (Sudden load increase)");
//                Console.WriteLine("3. Mixed Workload Test (Realistic usage patterns)");
//                Console.WriteLine("4. Breaking Point Test (Find maximum capacity)");
//                Console.Write("Enter choice (1-4): ");

//                var choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        Console.WriteLine("Running Basic Capacity Test...");
//                        loadTester.RunBasicCapacityTest();
//                        break;
//                    case "2":
//                        Console.WriteLine("Running Spike Test...");
//                        loadTester.RunSpikeTest();
//                        break;
//                    case "3":
//                        Console.WriteLine("Running Mixed Workload Test...");
//                        loadTester.RunMixedWorkloadTest();
//                        break;
//                    case "4":
//                        Console.WriteLine("Running Breaking Point Test...");
//                        Console.WriteLine("Note: Run this for each framework separately by changing the URL");
//                        loadTester.RunBreakingPointTest();
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice. Running Basic Capacity Test...");
//                        loadTester.RunBasicCapacityTest();
//                        break;
//                }

//                Console.WriteLine();
//                Console.WriteLine("Load test completed! Check the results folder for detailed reports.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error running load test: {ex.Message}");
//            }
//            finally
//            {
//                loadTester.Dispose();
//            }

//            Console.WriteLine("Press any key to exit...");
//            Console.ReadKey();
//        }
//    }
//}