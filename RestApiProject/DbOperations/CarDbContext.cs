using Microsoft.EntityFrameworkCore;

namespace RestApiProject.DbOperations
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}