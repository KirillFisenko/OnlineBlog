using Microsoft.EntityFrameworkCore;

namespace OnlineBlog.Server.Data
{
    /// <summary>
    /// Контекст БД для пользователей и постов
    /// </summary>
    public class AppDataContext : DbContext
    {
        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Таблица постов
        /// </summary>
        public DbSet<News> News { get; set; }


        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
