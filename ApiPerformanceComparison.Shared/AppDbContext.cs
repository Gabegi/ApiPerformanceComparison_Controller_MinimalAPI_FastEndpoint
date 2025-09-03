using Microsoft.EntityFrameworkCore;

namespace ApiPerformanceComparison.Shared
{

        public class AppDbContext(DbContextOptions options) : DbContext(options)
        {
        public DbSet<Product> Products { get; set; }
        }
    
}
