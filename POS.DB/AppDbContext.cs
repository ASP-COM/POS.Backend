using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using System.Diagnostics;

namespace POS.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
