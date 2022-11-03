using Microsoft.EntityFrameworkCore;
using urlshortner.Models;

namespace urlshortner.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Access> Accesses { get; set; }
    }
}
