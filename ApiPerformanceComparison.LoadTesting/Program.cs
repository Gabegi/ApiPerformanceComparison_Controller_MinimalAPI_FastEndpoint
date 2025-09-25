using ApiPerformanceComparison.LoadTesting.LoadTesting;

public class Program
{
    public static void Main(string[] args)
    {
        using var loadTester = new ApiFrameworkLoadTests();

        var endpoints = new[]
        {
            "http://localhost:5001", // Controllers
            "http://localhost:5002", // Minimal API
            "http://localhost:5003"  // FastEndpoints
        };

        Console.WriteLine("API Framework Load Testing (Laptop-Friendly)");
        Console.WriteLine("================================================");

        try
        {
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine($"➡️ Running Basic Capacity Test for {endpoint}...");
                loadTester.RunBasicCapacityTest(endpoint);

                Console.WriteLine($"➡️ Running Spike Test for {endpoint}...");
                loadTester.RunSpikeTest(endpoint);

                Console.WriteLine($"➡️ Running Mixed Workload Test for {endpoint}...");
                loadTester.RunMixedWorkloadTest(endpoint);

                Console.WriteLine($"➡️ Running Breaking Point Test for {endpoint}...");
                loadTester.RunBreakingPointTest(endpoint);

                Console.WriteLine($"✅ Finished all tests for {endpoint}.\n");
            }

            Console.WriteLine("🎉 All load tests completed successfully!");
            Console.WriteLine("Check the report folders (ending in _safe) for results.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error running load tests: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
