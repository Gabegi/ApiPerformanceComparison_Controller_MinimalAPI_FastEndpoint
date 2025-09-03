namespace ApiPerformanceComparison.Shared
{
    public static class QuickSeeder
    {
        public static List<Product> SeedProducts(int count = 50_000)
        {
            return Enumerable.Range(1, count).Select(i =>
                new Product
                {
                    Id = i,
                    Name = $"Product {i:D6}",
                    Description = "Standard product description",
                    Price = (i % 100) + 1,     // 1–100
                    Category = $"Category{i % 5}",
                    CreatedAt = DateTime.UtcNow.AddDays(-i % 365),
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = i % 2 == 0
                }
            ).ToList();
        }
    }


}
