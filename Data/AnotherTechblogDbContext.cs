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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the entity mappings here
        }

    }
}