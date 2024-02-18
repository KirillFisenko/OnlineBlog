using Microsoft.EntityFrameworkCore;

namespace OnlineBlog.Server.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
