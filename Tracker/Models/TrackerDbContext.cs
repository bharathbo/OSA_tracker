using Microsoft.EntityFrameworkCore;

namespace Tracker.Models
{
    public class TrackerDbContext : DbContext
    {
        public TrackerDbContext(DbContextOptions<TrackerDbContext> options)
            : base(options)
        {
        }

        // Add DbSet for Expense
        public DbSet<Expense> Expenses { get; set; }
    }
}
