using GestiuneCD.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestiuneCD.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }
        public DbSet<CD> CDs { get; set; }
    }
}
