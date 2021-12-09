using Microsoft.EntityFrameworkCore;

namespace MihailinStrikesBack.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Post2Sprav> Post2Sprav { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base (options)
        {
            Database.EnsureCreated();
        }
    }
}
