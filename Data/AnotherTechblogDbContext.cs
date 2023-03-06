using Microsoft.EntityFrameworkCore;
using AnotherTechblog.Models;

namespace AnotherTechblog.Data
{
    public class AnotherTechblogDbContext : DbContext
    {
        public AnotherTechblogDbContext (DbContextOptions<AnotherTechblogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Encryption> Encryption { get; set; } = default!;
    }
}
