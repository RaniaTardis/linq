using Microsoft.EntityFrameworkCore;

namespace Monday.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Emp> Emps { get; set; }
    }
}
