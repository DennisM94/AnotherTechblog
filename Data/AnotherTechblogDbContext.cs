using Microsoft.EntityFrameworkCore;

namespace AnotherTechblog.Data
{
    public class AnotherTechblogsContext : DbContext
    {
        public AnotherTechblogsContext (DbContextOptions<AnotherTechblogsContext> options)
            : base(options)
        {
        }

        public DbSet<AnotherTechblog.Models.BlogPost> Movie { get; set; } = default!;
    }
}
